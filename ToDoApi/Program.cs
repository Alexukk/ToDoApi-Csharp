using Scalar.AspNetCore;
using System.Data;
using ToDoApi.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var conString = builder.Configuration.GetConnectionString("ToDoDB");  // Setting Up a DB PATH 


// Add services to the container.
builder.Services.AddDbContext<ToDoContext>(options =>
{
    if (conString != null)
    {
        options.UseSqlite(conString);
    }
});


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapScalarApiReference();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
