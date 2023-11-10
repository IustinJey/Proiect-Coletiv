using API;
using API.Data;
using MySql.Data.EntityFrameworkCore.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MySqlConnector>(_ => 
{
    MySqlConnector(builder.Configuration.GetConnectionString"DefaultConnection")
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
