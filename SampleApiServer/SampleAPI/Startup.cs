using System.Text.Json.Serialization;
using SampleAPI.Component.RepositoryLayer.Repository;
using SampleAPI.Component.ServiceLayer.Services;
using SampleAPI.Core.DomainLayer.Data;
using SampleAPI.Core.ServiceLayer.Helpers;

namespace SampleAPI.Core.PresentationLayer
{
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddCors();
            //services.AddControllers();
            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // ignore omitted parameters on models to enable optional params (e.g. User update)
                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // configure strongly typed settings object
            services.Configure<DbSettings>(Configuration.GetSection("DbSettings"));

            #region Register Dependent Types (Services) with IoC Container in HERE
            // configure DI for application services
            services.AddSingleton<DataContext>();
            services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<UserService, UserService>();

            #endregion
        }

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure HTTP request pipeline (Middleware)
            if (env.IsDevelopment())
            {
                #region Swagger
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI();
                /*
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitecture.WebApi");
                });
                */
                #endregion
                app.UseDeveloperExceptionPage();
            }
            
            // Ensure database and tables exist
            {
                //using var scope = app.ApplicationServices.CreateScope();
                //var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                //await context.Init();
            }
            
            // Global cors policy
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
           
            // Global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
