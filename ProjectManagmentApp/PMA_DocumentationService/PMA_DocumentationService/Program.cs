using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PMA_DocumentationService.Data;
using PMA_DocumentationService.Middleware;
using PMA_DocumentationService.Models;
using PMA_DocumentationService.Repositories;
using PMA_DocumentationService.Repositories.Interfaces;
using PMA_DocumentationService.Services.DocumentServices;


var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration.AddJsonFile("appHost.json").Build();

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<BaseAppHost>(Configuration);
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(Configuration.GetConnectionString("DocumentationServiceDatabase")));
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
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
app.UseStaticFiles();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorizationMiddleware();

app.MapControllers();

app.Run();
