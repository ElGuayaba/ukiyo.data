using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Ukiyo.Application.Implementation.Service;

namespace Ukiyo.Api.WebApi.Configuration
{
	public static partial class DependencyInjectionExtension
    {
        private static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<FaceAnalysisService>()
                .AddClasses(classes =>
                    classes.Where(c => c.Name.EndsWith("Service")))
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            
            return services;
        }
    }
}