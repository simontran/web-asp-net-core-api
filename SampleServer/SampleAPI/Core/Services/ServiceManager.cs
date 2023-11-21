using SampleAPI.Component.RepositoryLayer.Repository;
using SampleAPI.Component.ServiceLayer.Services;
using SampleAPI.Component.Users.Models.DDL;

namespace SampleAPI.Core.ServiceLayer.Services
{
    public static class ServiceManager
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            #region Register Dependent Types (Services) with IoC Container in HERE
            services.AddSingleton<UserTable>();
            services.AddScoped<UserRepository>();
            services.AddScoped<UserService>();

            #endregion
        }
    }
}