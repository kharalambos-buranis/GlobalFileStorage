using FluentValidation;
using GlobalFileStorage.Api.Common.Entities;
using GlobalFileStorage.Api.Domain.RepositoryInterfaces;
using GlobalFileStorage.Api.Domain.ServiceLayerInterfaces;
using GlobalFileStorage.Api.Infrastructure.Database;
using GlobalFileStorage.Api.Infrastructure.Repositories;
using GlobalFileStorage.Api.Infrastructure.Services;
using GlobalFileStorage.Api.Infrastructure.Services.Middlewares;
using GlobalFileStorage.Api.Infrastructure.Services.ServiceLayers;
using GlobalFileStorage.Api.Infrastructure.Services.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Minio;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TenantDbContext>(
      optionsBuilder => 
      {
          optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
      });
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IFileItemRepository, FileItemRepository>();
builder.Services.AddScoped<IUsageStatsRepository,UsageStatsRepository>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileItemService, FileItemService>();
builder.Services.AddScoped<IUsageStatsService, UsageStatsService>();




builder.Services.AddScoped<IFileStorageService, MinioFileStorageService>();

builder.Services.AddSingleton<IMinioClient>(sp =>
{
    return new MinioClient()
        .WithEndpoint("localhost:9000") // or your MinIO URL
        .WithCredentials("minio", "minio123") // from docker-compose
        .Build();
});

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddSingleton<TokenProvider>();
builder.Services.AddAuthorization();


builder.Services.AddValidatorsFromAssemblyContaining<RegisterTenantValidator>(includeInternalTypes: true);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters 
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
        };
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
