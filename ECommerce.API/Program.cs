using ECommerce.API;
using ECommerce.API.Extensions;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Identity.Entities;
using ECommerce.UseCases;
using ECommerce.UseCases.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApi();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddUseCases();

builder.Services.Configure<UrlSettings>(builder.Configuration.GetSection("UrlSettings"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();

    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Files")),
    RequestPath = "/Files"
});

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

await app.SeedAndMigrateDataAsync();


app.Run();


