using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ukiyo.Infrastructure.Contract.Client;

namespace Ukiyo.Infrastructure.Implementation.Client
{
	public class UkiyoApiClient : IUkiyoApiClient
    {
        protected readonly HttpClient Client;

        public UkiyoApiClient(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri("https://api.ukiyo.fosc.space");           
			client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJndWF5YWJhIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiI1ZTg3NTBjNS04MmYwLTQ3Y2YtOWNiYy0wNTY1NWYzYzI3NGIiLCJqdGkiOiJhMzgwYWIyNC1jYTBjLTQ5NWEtOGJmYi02NjhjY2NjZTgxZGEiLCJpc3MiOiJodHRwczovL2FwaS51eWlrby5mb3NjLnNwYWNlIn0.uatKFZUp00kuWp8r_ngfwuNtwDPvEEWrj4_1SrkVcpU"); 
            Client = client;
        }

		public async Task<HttpResponseMessage> GetConsumptionByMonth(string buildingId)
		{
			return await Client.GetAsync($"/api/v1/Consumption/interval?buildingId=d37fc422-0462-4c48-a54c-846258d0944a&start=01/05/2018&finish=01/06/2018");
		}
	}
}