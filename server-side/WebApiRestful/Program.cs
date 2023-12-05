using WebApiRestful.Helpers;
using WebApiRestful.Infrastructure.Configuration;
using WebApiRestful.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
#region Add services to the container.
{
    var services = builder.Services;
    var env = builder.Environment;
    var config = builder.Configuration;

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    #region Add services mores at HERE
    // Configure DI for application services
    services.RegisterDI(config);
    services.RegisterTokenBear(config);

    #endregion
}
#endregion

var app = builder.Build();
#region Configure the HTTP request pipeline.
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    #region Configure mores at HERE
    // Global cors policy
    app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

    // Global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // Ensure database & tables exist
    app.DbInitialize();

    #endregion
}
#endregion

app.Run();