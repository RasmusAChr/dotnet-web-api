using dotnet_web_api.Data;
using dotnet_web_api.ExceptionHandlers;
using dotnet_web_api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Lives throughout the whole request.
// The instance lives until the final result is returned.
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IColumnService, ColumnService>();
builder.Services.AddScoped<IBoardService, BoardService>();

// builder.Services.AddFluentValidationAutoValidation(); // Deprecated
builder.Services.AddValidatorsFromAssemblyContaining<Program>(); // Scans and registers all validators in project

// Add exception handlers for Fluent Validation to avoid plain http 500
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<ConflictExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();