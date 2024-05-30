using SUDLife_Aadarsh.Controllers;
using SUDLife_Aadarsh.Models.Request;
using SUDLife_Aadarsh.ServiceLayer;
using SUDLife_CallThirdPartyAPI;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ClsCommonOperations>();
builder.Services.AddScoped<ThirdPartyAPI>();
builder.Services.AddScoped<ClsAadarsh>();
builder.Services.AddScoped<ClsSDEBasePremiumRequest>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
