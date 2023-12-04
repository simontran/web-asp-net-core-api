using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiRestful.Data.DbContext;
using WebApiRestful.Data.Repositories.Component;

namespace WebApiRestful.Infrastructure.Extensions
{
    public static class DbManager
    {
        /// <summary>
        /// Database Initialize
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost DbInitialize(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbService = scope.ServiceProvider.GetRequiredService<NpgDbContext>();
                var tblService = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                try
                {
                    _ = dbService.CreateDatabase();
                    #region Add more init tables at HERE
                    tblService.Todos.InitAsync();

                    #endregion
                }
                catch
                {
                    // log errors or ...
                    throw;
                }
            }

            return host;
        }

        /// <summary>
        /// Migrate Database
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbService = scope.ServiceProvider.GetRequiredService<NpgDbContext>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                try
                {
                    _ = dbService.CreateDatabase();
                    migrationService.ListMigrations();
                    migrationService.MigrateUp();
                }
                catch
                {
                    //log errors or ...
                    throw;
                }
            }
            return host;
        }
    }
}
