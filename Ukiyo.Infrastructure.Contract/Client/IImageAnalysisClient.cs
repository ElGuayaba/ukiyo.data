using System.Net.Http;
using System.Threading.Tasks;
using Ukiyo.Infrastructure.Contract.Parameters;

namespace Ukiyo.Infrastructure.Contract.Client
{
	public interface IImageAnalysisClient
	{
		Task<HttpResponseMessage> MakeRequest(ImageAnalysisFeatures feature, string langCode, byte[] body);
	}
}
