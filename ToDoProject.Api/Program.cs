using ToDoProject.Api.Extension;
using ToDoProject.CrossCutting.Logger.Abstract;
using ToDoProject.CrossCutting.Logger.Concrete;
using ToDoProject.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services
    .AddApplicationServices()
    .AddDataAccessServices()
    .AddCrossCuttingServices()
    .AddTransactionManager<PostgreSqlDbContext>()
    .AddUnitOfWork<PostgreSqlDbContext>()
    .AddDatabaseConnections();

// Jwt
builder.Services.AddJwtAuthentication();
builder.Services.AddCustomAuthorization();

// Logger 
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Swagger/OpenAPI
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

// Middleware
app.UseAuthentication();
app.UseAuthorization();

var loggerService = app.Services.GetRequiredService<ILoggerService>();
GlobalLogger.Configure(loggerService);

app.UseApplicationMiddleware();
app.MapControllers();

app.Run();