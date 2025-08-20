using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Domain.ModelViews;
using MinimalApi.Domain.Services;
using MinimalApi.Infra.Db;
using MinimalApi.Infra.Interfaces;
using System.Text;

namespace MinimalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            
            builder.Services.AddDbContext<DbContexto>();
            builder.Services.AddScoped<IAdministratorService, AdministratorService>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddSingleton<IJWTService, JWTService>();

            builder.Services.AddSwaggerGen(options =>
            {
                var scheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insert the JWT Token here"
                };
                options.AddSecurityDefinition("Bearer", scheme);

                var requirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                };
                options.AddSecurityRequirement(requirement);
            });

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateAudience = builder.Configuration["Jwt:Audience"].Equals("true"),
                    ValidateIssuer = builder.Configuration["Jwt:Issuer"].Equals("true"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/", () => Results.Json(new Home()));
            app.Run();
        }
    }
}


//builder.Services.AddDbContext<DbContexto>(options =>
//{
//    string? connString = builder.Configuration.GetConnectionString("mysql");
//    if (connString != null)
//    {
//        options.UseMySql(connString, ServerVersion.AutoDetect(connString));
//    }
//});