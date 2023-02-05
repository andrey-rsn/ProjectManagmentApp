using Microsoft.EntityFrameworkCore;
using PMA_WorkTimeService.Data;
using PMA_WorkTimeService.Middleware;
using PMA_WorkTimeService.Repositories;
using PMA_WorkTimeService.Services;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IWorkTimeRepository,WorkTimeRepository>();
builder.Services.AddScoped<IWorkTimeService, WorkTimeService>();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(Configuration.GetConnectionString("WorkTimeDatabase")));
builder.Services.AddHttpClient("authClient", c =>
{
    c.BaseAddress = new Uri("http://localhost:5069");
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

app.UseHttpsRedirection();


app.MapControllers();

app.UseAuthorizationMiddleware();

app.Run();
