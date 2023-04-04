using Microsoft.Extensions.Configuration;
using PMA_AnalyticsService.Middleware;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("authClient", c =>
{
    c.BaseAddress = new Uri(Configuration.GetConnectionString("IdentityService"));
});
builder.Services.AddHttpClient("tasksServiceClient", c =>
{
    c.BaseAddress = new Uri(Configuration.GetConnectionString("TasksService"));
});
builder.Services.AddHttpClient("projectsServiceClient", c =>
{
    c.BaseAddress = new Uri(Configuration.GetConnectionString("ProjectsService"));
});
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorizationMiddleware();

app.MapControllers();

app.Run();
