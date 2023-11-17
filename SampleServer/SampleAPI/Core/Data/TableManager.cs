using SampleAPI.Component.DDL;

namespace SampleAPI.Core.DomainLayer.Data
{
    public static class TableManager
    {
        public static IHost TableInit(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<UserTable>();
                try
                {
                    userService.CreateTable();
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
