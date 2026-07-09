using ECommerce.API;
using ECommerce.API.Extensions;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure;
using ECommerce.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApi();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddUseCases();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

await app.SeedAndMigrateDataAsync();


app.Run();


