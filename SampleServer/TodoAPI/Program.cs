using System.Text.Json.Serialization;
using TodoAPI.Component.Helpers;
using TodoAPI.Core.Common.Helpers;
using TodoAPI.Core.DomainLayer.Data;

var builder = WebApplication.CreateBuilder(args);
// JWT Authentication
JwtAuthentication.ConfigureService(builder);

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
    services.Configure<AppSettings>(builder.Configuration.GetSection("DbSettings"));
    services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

    // Configure DI for application services
    services.AddSingleton<DataContext>();
    // Ensure database exist
    services.AddSingleton<DataInitialize>();
    services.AddInfrastructure();
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

    app.MapControllers();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();
}

// Ensure database exist
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataInitialize>();
    context.CreateDatabase();
}
// Ensure table exist
app.TableInit();

app.Run();