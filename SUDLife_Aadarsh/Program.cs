using SUDLife_Aadarsh.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using SUDLife_DataRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Configuration.AddJsonFile("C:/Users/40421/Documents/Sud Projects/PartnerIntegrationMicroServicesNew/SUDLife_DataRepo/appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "SUDLife_DataRepo/appsettings.json"), optional: false, reloadOnChange: true);
builder.Services.AddDataAccess(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

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
