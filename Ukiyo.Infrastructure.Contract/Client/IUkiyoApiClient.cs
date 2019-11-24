using System.Net.Http;
using System.Threading.Tasks;

namespace Ukiyo.Infrastructure.Contract.Client
{
	public interface IUkiyoApiClient
    {
		Task<HttpResponseMessage> GetConsumptionByMonth(string buildingId);
	}
}