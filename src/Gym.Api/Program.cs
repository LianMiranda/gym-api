using Gym.Infrastructure.Config;
using Gym.Infrastructure.Database;
using Gym.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Gym.Infrastructure")); });
builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/", () => "Hello World");

app.Run();