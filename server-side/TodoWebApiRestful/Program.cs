using TodoWebApiRestful.Common.InfrastructureLayer.Configuration;
using TodoWebApiRestful.Common.InfrastructureLayer.Helpers;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
{
    var services = builder.Services;
    var env = builder.Environment;
    var config = builder.Configuration;

    // Configure DI for application services
    services.AddInfrastructure(config);
}

var app = builder.Build();
// Configure the HTTP request pipeline.
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Global cors policy
    app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

    // Global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // Ensure database & tables exist
    app.DbInitialize();

    app.UseAuthorization();

    app.MapControllers();
}
app.Run();