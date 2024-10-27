using AspNetCoreRateLimit;
using DogHouseService.Application.Mappings;
using DogHouseService.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Added automapper
builder.Services.AddAutoMapper(typeof(DogMappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<CreateDogCommandValidator>();
builder.Services.AddFluentValidationAutoValidation();

//Limit requests per second
builder.Services.Configure<IpRateLimitOptions>(options =>
options.GeneralRules = new List<RateLimitRule> 
{ 
    new RateLimitRule
    {
        Endpoint = "*",
        Limit = 10,
        Period = "1s"
    }
}
);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseIpRateLimiting();
app.UseAuthorization();

app.MapControllers();

app.Run();
