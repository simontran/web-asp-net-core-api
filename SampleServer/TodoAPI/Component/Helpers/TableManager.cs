using TodoAPI.Component.DomainLayer.Models.DDL;

namespace TodoAPI.Component.Helpers
{
    public static class TableManager
    {
        public static IHost TableInit(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var user = scope.ServiceProvider.GetRequiredService<UserTable>();
                var todo = scope.ServiceProvider.GetRequiredService<TodoTable>();
                try
                {
                    user.CreateTable();
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
