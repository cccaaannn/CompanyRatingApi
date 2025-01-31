using System.Text.Json.Serialization;
using CompanyRatingApi;
using CompanyRatingApi.Middlewares;
using CompanyRatingApi.Shared.Attributes.Injectable;
using CompanyRatingApi.Shared.Auth;
using CompanyRatingApi.Shared.Handlers;
using CompanyRatingApi.Shared.Middlewares.ExceptionMiddleware;
using CompanyRatingApi.Shared.Persistence;
using CompanyRatingApi.Shared.Swagger;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(9, 0, 1))
    );
});

// Add JWT Auth
builder.Services.AddJwtAuth(builder.Configuration);

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Register FluentValidation validators
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Register injectable classes
builder.Services.AddInjectablesFromAssembly(typeof(Program).Assembly);

// Register handlers
builder.Services.AddHandlersFromAssembly(typeof(Program).Assembly);

// Register Application services
builder.Services.RegisterApplicationServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.DocExpansion(DocExpansion.None));
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts(); // HTTP Strict Transport Security
    app.UseHttpsRedirection(); // Enforce HTTPS 
}

// Exception handling
app.UseMiddleware<ExceptionMiddleware>();

// Authentiacation
app.UseAuthentication();
app.UseAuthorization();

// Current user middleware, should be after authentication
app.UseMiddleware<CurrentUserMiddleware>();

app.MapControllers();

app.Run();