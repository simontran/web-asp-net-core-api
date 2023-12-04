using WebApiRestful.Domain.Entities.Common;
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
    // Configure strongly typed settings object
    services.Configure<Database>(config.GetSection("DbSettings"));
    // Configure DI for application services
    services.AddInfrastructure(config);

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

    app.UseAuthorization();

    app.MapControllers();

    #region Configure mores at HERE
    // Ensure database & tables exist
    app.DbInitialize();

    #endregion
}
#endregion

app.Run();