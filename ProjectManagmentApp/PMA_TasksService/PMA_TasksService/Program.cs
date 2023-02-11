using Microsoft.EntityFrameworkCore;
using PMA_TasksService.Data;
using PMA_TasksService.Extensions;
using PMA_TasksService.Middleware;
using PMA_TasksService.Repositories;
using PMA_TasksService.Repositories.Interfaces;
using PMA_TasksService.Services.CommentServices;
using PMA_TasksService.Services.TaskCommentsServices;
using PMA_TasksService.Services.UserTaskServices;
using PMA_TasksService.Services.UserTaskStatusServices;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(Configuration.GetConnectionString("TaskServiceDatabase")));
builder.Services.AddScoped<IUserTaskRepository, UserTaskRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ITaskCommentsRepository, TaskCommentsRepository>();
builder.Services.AddScoped<IUserTaskStatusRepository, UserTaskStatusRepository>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IUserTaskService,UserTaskService>();
builder.Services.AddScoped<ICommentService,CommentService>();
builder.Services.AddScoped<ITaskCommentsService,TaskCommentsService>();
builder.Services.AddScoped<IUserTaskStatusService,UserTaskStatusService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("authClient", c =>
{
    c.BaseAddress = new Uri(Configuration.GetConnectionString("IdentityService"));
});
builder.Services.AddCors();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
