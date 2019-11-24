using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Ukiyo.Infrastructure.Contract.Client;
using Ukiyo.Infrastructure.Contract.Parameters;

namespace Ukiyo.Infrastructure.Implementation.Client
{
	public class ImageAnalysisClient : IImageAnalysisClient
	{
		protected readonly HttpClient HttpClient;

		public ImageAnalysisClient(HttpClient httpClient, IConfiguration configuration)
		{
			httpClient.BaseAddress = new Uri(configuration["COMPUTER_VISION_ENDPOINT"]);
			httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", configuration["COMPUTER_VISION_SUBSCRIPTION_KEY"]);

			HttpClient = httpClient;
		}

		public async Task<HttpResponseMessage> MakeRequest(ImageAnalysisFeatures feature, string langCode, byte[] body)
		{
			var queryString = HttpUtility.ParseQueryString(string.Empty);

			// Request parameters
			queryString["visualFeatures"] = nameof(feature);
			queryString["language"] = nameof(langCode).ToLower();

			using (var content = new ByteArrayContent(body))
			{
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				return await HttpClient.PostAsync(queryString.ToString(), content);
			}

		}
	}
}
