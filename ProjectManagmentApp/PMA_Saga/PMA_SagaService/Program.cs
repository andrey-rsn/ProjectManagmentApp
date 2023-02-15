

using PMA_SagaService.Middleware;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddHttpClient("tasksServiceClient", c =>
{
    c.BaseAddress = new Uri(Configuration.GetConnectionString("TasksService"));
});
builder.Services.AddHttpClient("identityServiceClient", c =>
{
    c.BaseAddress = new Uri(Configuration.GetConnectionString("IdentityService"));
});


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
