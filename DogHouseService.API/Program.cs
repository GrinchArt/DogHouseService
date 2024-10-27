using DogHouseService.Application.Mappings;
using DogHouseService.Application.Queries.GetDogs;
using DogHouseService.Application.Validators;
using DogHouseService.Domain.Interfaces;
using DogHouseService.Infrastructure.Data;
using DogHouseService.Infrastructure.MiddleWare;
using DogHouseService.Infrastructure.RateLimiting;
using DogHouseService.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Added automapper
builder.Services.AddAutoMapper(typeof(DogMappingProfile));
//Fluent Validator
builder.Services.AddValidatorsFromAssemblyContaining<CreateDogCommandValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDogRepository, DogRepository>();

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetDogsQueryHandler).Assembly));


//Limit requests per second
builder.Services.Configure<TokenBucketRateLimitingOptions>(options =>
{
    options.BucketCapacity = 10;
    options.TokensPerInterval = 1;
    options.Interval = TimeSpan.FromSeconds(1);
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.UseMiddleware<TokenBucketRateLimitingMiddleware>();

app.MapControllers();

app.Run();
