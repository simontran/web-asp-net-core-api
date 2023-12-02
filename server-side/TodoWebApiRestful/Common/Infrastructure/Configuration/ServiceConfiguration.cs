using System.Text.Json.Serialization;
using TodoWebApiRestful.Common.DomainLayer.Data;
using TodoWebApiRestful.Common.InfrastructureLayer.Helpers;
using TodoWebApiRestful.Common.PersistenceLayer.Repositories;
using TodoWebApiRestful.Common.ServiceLayer.Services;
using TodoWebApiRestful.Component.ServiceLayer.Services;

namespace TodoWebApiRestful.Common.InfrastructureLayer.Configuration
{
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Configure DI for application services
        /// </summary>
        /// <param name="services"></param>
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Register Dependent Types (Services) with IoC Container in HERE
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors();
            services.AddControllers().AddJsonOptions(x =>
            {
                // Serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // Ignore omitted parameters on models to enable optional params (e.g. User update)
                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            // Configure strongly typed settings object
            services.Configure<AppSettings>(configuration.GetSection("DbSettings"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // DataContext
            services.AddSingleton<DataContext>();

            // Ensure database exist
            services.AddSingleton<DbService>();

            // Register DI of Repository
            services.AddScoped<IRepository, Repository>();

            #endregion

            #region Register DI for all component in HERE
            // Todos
            services.AddSingleton<TodoTblService>();
            services.AddScoped<TodoService>();

            #endregion
        }
    }
}