using Microsoft.EntityFrameworkCore;
using WorkshopNo1.Repository;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration; // get configuration from appsettings.json

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(configuration.GetConnectionString("ConnectionString"))); // get connection string from appsettings.Development.json

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