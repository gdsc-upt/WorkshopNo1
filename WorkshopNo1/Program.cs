using Microsoft.EntityFrameworkCore;
using WorkshopNo1;
using WorkshopNo1.Entities.Students;
using WorkshopNo1.Repository;
using WorkshopNo1.Services;
using WorkshopNo1.Utils;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration; // get configuration from appsettings.json

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories(configuration);

var config = configuration.GetSection("EmailConfig");
builder.Services.Configure<EmailConfig>(config);

//builder.Services.AddScoped<IRandomService, RandomService>();
builder.Services.AddSingleton<IRandomService, RandomService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();