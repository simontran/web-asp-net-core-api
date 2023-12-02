using TodoWebApiRestful.Common.ServiceLayer.Services;
using TodoWebApiRestful.Component.ServiceLayer.Services;

namespace TodoWebApiRestful.Common.InfrastructureLayer.Configuration
{
    public static class DbConfiguration
    {
        /// <summary>
        /// Ensure database and tables exist
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost DbInitialize(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<DbService>();
                    context.CreateDatabase();

                    var todo = scope.ServiceProvider.GetRequiredService<TodoTblService>();
                    todo.CreateTable();
                }
                catch
                {
                    // log errors or ...
                    throw;
                }
            }

            return host;
        }
    }
}