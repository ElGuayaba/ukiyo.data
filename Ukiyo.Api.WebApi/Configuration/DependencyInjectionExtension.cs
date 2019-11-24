using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Ukiyo.Api.WebApi.Identity;
using Ukiyo.Api.WebApi.Service.Contract;
using Ukiyo.Api.WebApi.Service.Implementation;

namespace Ukiyo.Api.WebApi.Configuration
{
	public static partial class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            // Web Api
            services.AddCustomAuthentication(configuration);
            services.AddApiVersioning(configuration);
            services.AddCustomIdentity(configuration);
			services.AddSwagger(configuration);
            
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<ITokenLoginService, TokenLoginService>();

			// Application
			services.AddApplicationServices(configuration);

            // Infrastructure
            services.AddInfrastructureClients(configuration);


            return services;
        }

        private static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidIssuer = configuration["JWT_TOKEN_ISSUER"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT_TOKEN_SECURITY_KEY"]))
                    };
                });

            return services;
        }

        private static IServiceCollection AddApiVersioning(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            
            return services;
        }

        private static IServiceCollection AddCustomIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

		private static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Ukiyo",
					Description = "An Azure Cognitive Services Wrapper API",
					Contact = new OpenApiContact
					{
						Name = "Jose Alamo",
						Email = string.Empty
					},
					License = new OpenApiLicense
					{
						Name = string.Empty,
					}
				});
			});

			return services;
		}
	}
}