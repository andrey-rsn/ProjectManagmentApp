using Microsoft.EntityFrameworkCore;
using PMA_ProjectsService.Data;
using PMA_ProjectsService.Extensions;
using PMA_ProjectsService.Middleware;
using PMA_ProjectsService.Repositories;
using PMA_ProjectsService.Repositories.Interfaces;
using PMA_ProjectsService.Services.EmployeesAttachedToProjectsServices;
using PMA_ProjectsService.Services.ProjectServices;
using PMA_ProjectsService.Services.ProjectsTasksServices;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(Configuration.GetConnectionString("ProjectsServiceDatabase")));

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IEmployeesAttachedToProjectsRepository, EmployeesAttachedToProjectsRepository>();
builder.Services.AddScoped<IProjectsTasksRepository, ProjectsTasksRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IEmployeesAttachedToProjectsService, EmployeesAttachedToProjectsService>();
builder.Services.AddScoped<IProjectsTasksService, ProjectsTasksService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("authClient", c =>
{
    c.BaseAddress = new Uri(Configuration.GetConnectionString("IdentityService"));
});
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

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
