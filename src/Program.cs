using System.Text.Json.Serialization;
using CompanyRateApi.Shared.Attributes.Injectable;
using CompanyRateApi.Shared.Handlers;
using CompanyRateApi.Shared.Middlewares.ExceptionMiddleware;
using CompanyRateApi.Shared.Persistence;
using CompanyRateApi.Shared.Swagger;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(builder.Configuration);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(9, 0, 1))
    );
});

// Add JWT Auth
// builder.Services.AddJwtAuth(builder.Configuration);

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Register FluentValidation validators
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Register injectable classes
builder.Services.AddInjectablesFromAssembly(typeof(Program).Assembly);

// Register handlers
builder.Services.AddHandlersFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

// Apply migrations
using var scope = app.Services.CreateScope();
await scope.ServiceProvider.GetRequiredService<DbContextBootstrap>().InitializeAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts(); // HTTP Strict Transport Security
    app.UseHttpsRedirection(); // Enforce HTTPS 
}

// Exception handling
app.UseMiddleware<ExceptionMiddleware>();

// app.MapIdentityApi<AppUser>();
// app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();