using Microsoft.EntityFrameworkCore;
using PMA_ProjectsService.Data;
using PMA_ProjectsService.Extensions;
using PMA_ProjectsService.Middleware;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(Configuration.GetConnectionString("ProjectsServiceDatabase")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorizationMiddleware();

app.MapControllers();

app.MigrateDatabase<ApplicationDbContext>((context, service) =>
{
    var logger = app.Services.GetService<ILogger<ApplicationDbContext>>();
    DatabaseSeed
                .SeedAsync(context, logger)
                .Wait();
});

app.Run();
