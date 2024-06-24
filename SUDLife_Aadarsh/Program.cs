using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SUDLife_Aadarsh.Models.Request;
using SUDLife_Aadarsh.ServiceLayer;
using System.Text;
using SUDLife_DataRepo;
using SUDLife_Logger;
using SUDLife_CallThirdPartyAPI;

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

//Added by Mayur for DI configuration
//builder.Configuration.AddJsonFile("C:/Users/40421/Documents/Sud Projects/PartnerIntegrationMicroServicesNew/SUDLife_DataRepo/appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "SUDLife_DataRepo/appsettings.json"), optional: false, reloadOnChange: true);
builder.Services.AddDataAccess(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ClsCommonOperations>();
builder.Services.AddScoped<ThirdPartyAPI>();
builder.Services.AddScoped<ClsAadarsh>();
builder.Services.AddScoped<ClsSDEBasePremiumRequest>();

string issuer = "CCDD18D8-49FF-43E5-8E6C-D0924C2BBE0C";
string audience = "B502C4CA-9895-419A-AF6C-65B5801CBDEA";
string symmetricSecurityKey = "463C24EE-EE45-4957-A682-3704AD7F91C7";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricSecurityKey))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

try
{
    Log.Information("Starting web host");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    ClsLoggerConfigurator.CloseLogger();
}
