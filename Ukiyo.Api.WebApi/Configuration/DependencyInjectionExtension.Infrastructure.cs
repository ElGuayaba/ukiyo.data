using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Scrutor;
using System;
using Ukiyo.Infrastructure.Contract.Client;
using Ukiyo.Infrastructure.Implementation.Client;

namespace Ukiyo.Api.WebApi.Configuration
{
	public static partial class DependencyInjectionExtension
	{
		private static IServiceCollection AddInfrastructureClients(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IComputerVisionClient>(factory => {
				var key = configuration["COMPUTER_VISION_SUBSCRIPTION_KEY"];
				var host = configuration["COMPUTER_VISION_ENDPOINT"];

				var credentials = new ApiKeyServiceClientCredentials(key);
				var client = new ComputerVisionClient(credentials);
				client.Endpoint = host;

				return client;
			});

			services.AddHttpClient<IImageAnalysisClient, ImageAnalysisClient>()
				.AddTransientHttpErrorPolicy(builder =>
					builder.WaitAndRetryAsync(3, retryCount =>
						TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

			services.AddHttpClient<IUkiyoApiClient, UkiyoApiClient>()
				.AddTransientHttpErrorPolicy(builder =>
					builder.WaitAndRetryAsync(3, retryCount =>
						TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

			services.Scan(scan => scan
				.FromAssemblyOf<ImageAnalysisClient>()
				.AddClasses(classes =>
					classes.Where(c => c.Name.EndsWith("Service")))
				.UsingRegistrationStrategy(RegistrationStrategy.Skip)
				.AsImplementedInterfaces()
				.WithScopedLifetime());

			return services;
		}
	}
}
