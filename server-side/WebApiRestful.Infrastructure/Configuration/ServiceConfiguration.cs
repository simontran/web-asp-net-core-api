using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebApiRestful.Data.DbContext;
using WebApiRestful.Data.Repositories.Component;
using WebApiRestful.Domain.Entities.Common;
using WebApiRestful.Infrastructure.Authentication;
using WebApiRestful.Service.Component;

namespace WebApiRestful.Infrastructure.Configuration
{
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Configure DI for application services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterDI(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure strongly typed settings object
            services.Configure<Database>(configuration.GetSection("DbSettings"));

            // DataContext
            services.AddSingleton<NpgDbContext>();

            // Ensure tables exist
            services.AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddPostgres()
                .WithGlobalConnectionString(configuration.ToString())
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region Add more Entity at HERE
            // Todo
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<ITodoService, TodoService>();

            // User
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            // TokenBear
            services.AddScoped<ITokenHandler, TokenHandler>();

            #endregion
        }
    }
}
