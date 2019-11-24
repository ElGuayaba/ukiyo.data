using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Ukiyo.Application.Contract.Service;
using Ukiyo.Common.Model;

namespace Ukiyo.Api.WebApi.Controllers.V1
{
	[Route("api/[controller]")]
    [ApiController]
    public class EnergyConsumptionController : ControllerBase
    {
		protected readonly ILogger<EnergyConsumptionController> Logger;
		protected readonly IForecastService ForecastService;

		public EnergyConsumptionController(ILogger<EnergyConsumptionController> logger, IForecastService consumptionService)
		{
			Logger = logger;
			ForecastService = consumptionService;
		}

		[HttpPost("predictPrice")]
		public async Task<IActionResult> PredictPriceByMonth([FromBody] ConsumptionDto consumption)
		{
			var prediction = ForecastService.PredictPrice(consumption);

			if (prediction.IsError)
			{
				return BadRequest();
			}

			return Ok(prediction.Value);
		}

		[HttpPost("predictEnergy")]
		public async Task<IActionResult> PredictEnergyByMonth([FromBody] ConsumptionDto consumption)
		{
			var prediction = ForecastService.PredictEnergy(consumption);

			if (prediction.IsError)
			{
				return BadRequest();
			}

			return Ok(prediction.Value);
		}

		[HttpPost("predictWater")]
		public async Task<IActionResult> PredictWaterByMonth([FromBody] ConsumptionDto consumption)
		{
			var prediction = ForecastService.PredictWater(consumption);

			if (prediction.IsError)
			{
				return BadRequest();
			}

			return Ok(prediction.Value);
		}

		[HttpPost("predictHeating")]
		public async Task<IActionResult> PredictHeatingByMonth([FromBody] ConsumptionDto consumption)
		{
			var prediction = ForecastService.PredictHeating(consumption);

			if (prediction.IsError)
			{
				return BadRequest();
			}

			return Ok(prediction.Value);
		}		
	}
}
