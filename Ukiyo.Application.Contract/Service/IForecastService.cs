using OperationResult;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ukiyo.Common.ErrorHandling;
using Ukiyo.Common.Model;

namespace Ukiyo.Application.Contract.Service
{
	public interface IForecastService
	{
		Result<PricePrediction, Error> PredictPrice(ConsumptionDto consumption);
		Result<PricePrediction, Error> PredictEnergy(ConsumptionDto consumption);
		Result<PricePrediction, Error> PredictHeating(ConsumptionDto consumption);
		Result<PricePrediction, Error> PredictWater(ConsumptionDto consumption);

		Task<Result<List<ConsumptionDto>, Error>> GetConsumption(string buildingID);
	}
}