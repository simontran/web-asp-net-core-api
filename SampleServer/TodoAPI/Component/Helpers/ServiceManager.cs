using TodoAPI.Component.DomainLayer.Models.DDL;
using TodoAPI.Component.RepositoryLayer.Repository;
using TodoAPI.Component.ServiceLayer.Services;

namespace TodoAPI.Component.Helpers
{
    public static class ServiceManager
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            #region Register Dependent Types (Services) with IoC Container in HERE
            services.AddSingleton<UserTable>();
            services.AddScoped<LoginRepository>();
            services.AddScoped<LoginService>();
            services.AddScoped<UserRepository>();
            services.AddScoped<UserService>();

            services.AddSingleton<TodoTable>();
            services.AddScoped<TodoRepository>();
            services.AddScoped<TodoService>();

            #endregion
        }
    }
}