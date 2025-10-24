using Asp.Versioning;
using Azure.Identity;
using Azure.Storage.Blobs;
using CursedVibes.Application.Behaviors;
using CursedVibes.Application.Characters.Commands.CreateCharacter;
using CursedVibes.Application.Characters.Queries.GetCharacter;
using CursedVibes.Application.Infrastructure.AutoMapper;
using CursedVibes.Domain.Interfaces;
using CursedVibes.Infrastructure.Context;
using CursedVibes.Infrastructure.Repositories;
using CursedVibes.WebAPI.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Configuration
// Load secrets from Azure Key Vault
builder.Configuration.AddAzureKeyVault(
    new Uri($"https://luckypenny-license-key.vault.azure.net/"),
    new DefaultAzureCredential());

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Read secret from config
string licenseKey = builder.Configuration["LicenseKey"]
    ?? throw new InvalidOperationException("License key for MediatR and Automapper is missing.");

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CursedVibes API",
        Version = "v1",
        Description = "API for cursed character management"
    });
});

// EF Core
builder.Services.AddDbContext<CursedVibesDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("CursedVibesDb");

    var sqlConnection = new SqlConnection(connectionString);

    // Get token from Microsoft Entra
    var credential = new DefaultAzureCredential();
    var token = credential.GetToken(
        new Azure.Core.TokenRequestContext(
            new[] { "https://database.windows.net/.default" }
        )
    );

    sqlConnection.AccessToken = token.Token;

    options.UseSqlServer(sqlConnection);
});

//Blob Storage
builder.Services.AddSingleton(sp =>
{
    var sceneContainerUrl = sp.GetRequiredService<IConfiguration>()["BlobStorage:SceneContainerUrl"]
        ?? throw new ArgumentNullException("SceneContainerUrl", "SceneContainerUrl is missing in configuration.");
    var credential = new DefaultAzureCredential();
    return new BlobContainerClient(new Uri(sceneContainerUrl), credential);
});


//Repositories
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<ISceneRepository, BlobSceneRepository>();

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.LicenseKey = licenseKey;
    cfg.RegisterServicesFromAssembly(typeof(GetCharacterByIdHandler).Assembly);
});

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.LicenseKey = licenseKey;
    cfg.AddMaps(typeof(AutoMapperProfile).Assembly);
});

//FluentValidation
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssemblyContaining<CreateCharacterCommandValidator>();


// API Versioning
var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// API Explorer for Swagger
apiVersioningBuilder.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CursedVibes API v1");
    });
    Console.WriteLine("Swagger UI available at: https://localhost:54916/swagger");
}

app.UseHttpsRedirection();
app.UseValidationExceptionHandler();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
