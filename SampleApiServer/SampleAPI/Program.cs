using System.Text.Json.Serialization;
using SampleAPI.Core.DomainLayer.Data;
using SampleAPI.Core.ServiceLayer.Helpers;
using SampleAPI.Component.RepositoryLayer.Repository;
using SampleAPI.Component.ServiceLayer.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
{
    var services = builder.Services;
    var env = builder.Environment;

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // Serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // Ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Configure strongly typed settings object
    services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));

    // Configure DI for application services
    #region Register Dependent Types (Services) with IoC Container in HERE
    services.AddSingleton<DataContext>();
    services.AddScoped<UserRepository, UserRepository>();
    services.AddScoped<UserService, UserService>();

    #endregion
}

var app = builder.Build();
// Ensure database and tables exist
{
    //using var scope = app.Services.CreateScope();
    //var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    //await context.Init();
}
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

    app.MapControllers();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}
app.Run();