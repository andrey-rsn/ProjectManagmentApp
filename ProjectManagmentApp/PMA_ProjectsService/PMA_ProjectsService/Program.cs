using PMA_ProjectsService.Data;
using PMA_ProjectsService.Extensions;
using PMA_ProjectsService.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
