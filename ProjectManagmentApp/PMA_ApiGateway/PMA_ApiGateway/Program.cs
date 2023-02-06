using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json",optional : false,reloadOnChange : true)
    .AddEnvironmentVariables();
builder.Services.AddCors();

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());


await app.UseOcelot();

app.Run();
