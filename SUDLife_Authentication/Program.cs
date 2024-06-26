using Serilog;
using SUDLife_Authentication.ServiceLayer;
using SUDLife_DataRepo;

var builder = WebApplication.CreateBuilder(args);
// Loading configuration

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .Build();

// Configuring Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

// Using Serilog for logging
builder.Host.UseSerilog();

builder.Configuration.AddJsonFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "SUDLife_DataRepo/appsettings.json"), optional: false, reloadOnChange: true);
builder.Services.AddDataAccess(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ClsAuthenticationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
