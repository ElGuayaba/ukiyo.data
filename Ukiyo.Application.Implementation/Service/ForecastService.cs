using Microsoft.Extensions.Logging;
using Microsoft.ML;
using OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Ukiyo.Application.Contract.Service;
using Ukiyo.Common.ErrorHandling;
using Ukiyo.Common.Model;
using Ukiyo.Infrastructure.Contract.Client;
using static OperationResult.Helpers;

namespace Ukiyo.Application.Implementation.Service
{
	public class ForecastService : IForecastService
	{
		protected readonly ILogger<FaceAnalysisService> Logger;
		protected readonly IUkiyoApiClient UkiyoApiClient;
		public static MLContext context { get; set; }


		public ForecastService(
			ILogger<FaceAnalysisService> logger,
			IUkiyoApiClient ukiyoApiClient)
		{
			Logger = logger;
			UkiyoApiClient = ukiyoApiClient;
			context = new MLContext();
		}

		public Result<PricePrediction, Error> PredictPrice(ConsumptionDto consumptionDto)
		{
			MonetaryConsumption consumption;

			if (consumptionDto.Electricity == 0)
			{
				consumption = MapConsumption(GetConsumption("d37fc422-0462-4c48-a54c-846258d0944a").GetAwaiter().GetResult().Value.FirstOrDefault());
			}
			else
			{
				consumption = MapConsumption(consumptionDto);
			}

			string modelPath = AppDomain.CurrentDomain.BaseDirectory + "ML_Models/PriceByMonth.zip";
			ITransformer mlModel = context.Model.Load(modelPath, out var modelInputSchema);
			var predEngine = context.Model.CreatePredictionEngine<MonetaryConsumption, PricePrediction>(mlModel);

			// Use model to make prediction on input data
			return predEngine.Predict(consumption);
		}

		public Result<PricePrediction, Error> PredictEnergy(ConsumptionDto consumptionDto)
		{
			MonetaryConsumption consumption;

			if (consumptionDto is null)
			{
				consumption = MapConsumption(GetConsumption("d37fc422-0462-4c48-a54c-846258d0944a").GetAwaiter().GetResult().Value.FirstOrDefault());
			}
			else
			{
				consumption = MapConsumption(consumptionDto);
			}

			string modelPath = AppDomain.CurrentDomain.BaseDirectory + "ML_Models/EnergyByMonth.zip";
			ITransformer mlModel = context.Model.Load(modelPath, out var modelInputSchema);
			var predEngine = context.Model.CreatePredictionEngine<MonetaryConsumption, PricePrediction>(mlModel);

			// Use model to make prediction on input data
			return predEngine.Predict(consumption);
		}

		public Result<PricePrediction, Error> PredictWater(ConsumptionDto consumptionDto)
		{
			MonetaryConsumption consumption;

			if (consumptionDto is null)
			{
				consumption = MapConsumption(GetConsumption("d37fc422-0462-4c48-a54c-846258d0944a").GetAwaiter().GetResult().Value.FirstOrDefault());
			}
			else
			{
				consumption = MapConsumption(consumptionDto);
			}

			string modelPath = AppDomain.CurrentDomain.BaseDirectory + "ML_Models/WaterByMonth.zip";
			ITransformer mlModel = context.Model.Load(modelPath, out var modelInputSchema);
			var predEngine = context.Model.CreatePredictionEngine<MonetaryConsumption, PricePrediction>(mlModel);

			// Use model to make prediction on input data
			return predEngine.Predict(consumption);
		}

		public Result<PricePrediction, Error> PredictHeating(ConsumptionDto consumptionDto)
		{
			MonetaryConsumption consumption;

			if (consumptionDto is null)
			{
				consumption = MapConsumption(GetConsumption("d37fc422-0462-4c48-a54c-846258d0944a").GetAwaiter().GetResult().Value.FirstOrDefault());
			}
			else
			{
				consumption = MapConsumption(consumptionDto);
			}

			string modelPath = AppDomain.CurrentDomain.BaseDirectory + "ML_Models/HeatingByMonth.zip";
			ITransformer mlModel = context.Model.Load(modelPath, out var modelInputSchema);
			var predEngine = context.Model.CreatePredictionEngine<MonetaryConsumption, PricePrediction>(mlModel);

			// Use model to make prediction on input data
			return predEngine.Predict(consumption);
		}

		private MonetaryConsumption MapConsumption(ConsumptionDto consumptionDto)
		{
			return new MonetaryConsumption
			{
				Electricity = consumptionDto.Electricity.ToString(),
				Heating = consumptionDto.Heating.ToString(),
				Month = consumptionDto.Date.Month,
				Water = consumptionDto.Water.ToString()
			};
		}

		public async Task<Result<List<ConsumptionDto>, Error>> GetConsumption(string buildingID)
		{
			var dataResponse = await UkiyoApiClient.GetConsumptionByMonth(buildingID);

			if (!dataResponse.IsSuccessStatusCode)
			{
				return Error(Common.ErrorHandling.Helpers.HttpError("Unsuccessful", System.Net.HttpStatusCode.BadRequest));
			}
			
			using var responseStream = await dataResponse.Content.ReadAsStreamAsync();

			var deserializedResponse = await JsonSerializer.DeserializeAsync<List<ConsumptionDto>>(responseStream);

			return Ok(deserializedResponse);
		}
	}
}