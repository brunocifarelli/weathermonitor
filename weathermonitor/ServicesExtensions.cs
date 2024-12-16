using APIWeather.Domain.Aggregate.CidadesFavoritas;
using APIWeather.Domain.Aggregate.Usuarios;
using APIWeather.Infrastructure.Repositories;
using APIWeather.WebAPP.Application.CidadesFavoritas;
using APIWeather.WebAPP.Application.Usuarios;
using APIWeather.WebAPP.Application.Weather;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using APIWeather.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace APIWeather.WebAPP
{
    internal static class ServiceExtensions
    {
        internal static IServiceCollection AddServiceRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICidadesFavoritasRepository, CidadesFavoritasRepository>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();

            return services;
        }

        internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherService, WeatherService>();
            services.AddScoped<ICidadesFavoritasService, CidadesFavoritasService>();
            services.AddScoped<IUsuariosService, UsuariosService>();

            return services;
        }
        internal static IServiceCollection AddServiceDbContext(this IServiceCollection services, string connectionStringMySql)
        {
            services.AddDbContext<APIDbContext>(x => x.UseSqlServer(
                connectionStringMySql));

            return services;
        }

        internal static IServiceCollection AddJwtAuthorization(this IServiceCollection services, IConfigurationSection jwtSettings)
        {

            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherMonitor API", Version = "v1" });

                // Configurar o esquema de autenticação JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT no formato: Bearer {token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                });
            });

            services.AddAuthorization();

            return services;
        }

        internal static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddControllers();

            return services;
        }
    }

}
