using POSystem.Infrastructure.Data;
using POSystem.Infrastructure.Extenstions;
using POSystem.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddInfrastructure()
    .AddApplication()
    .AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DbUpMigrationRunner
    .MigrateDatabase(app.Configuration);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();