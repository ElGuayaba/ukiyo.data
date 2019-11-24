using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Ukiyo.Api.WebApi.DTO;
using Ukiyo.Application.Contract.Service;

namespace Ukiyo.Api.WebApi.Controllers.V1
{
	[Route("api/faceAnalysis")]
    [ApiController]
    public class FaceAnalysisController : ControllerBase
    {
		protected readonly IFaceAnalysisService FaceAnalysisService;
		public FaceAnalysisController(IFaceAnalysisService faceAnalysisService)
		{
			FaceAnalysisService = faceAnalysisService;
		}

		[HttpPost("analyzeFromImage")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public void AnalyzeFromImage()
        {
			using (var reader = new StreamReader(Request.Body))
			{
				var body = reader.ReadToEnd();
			}
		}

		[HttpPost("analyzeFromUrl")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Produces("application/json", Type = typeof(List<ImageAnalysis>))]
		public async Task<IActionResult> AnalyzeFromUrl([FromBody] UrlRequest req)
		{
			var features = new List<VisualFeatureTypes>();

			features.Add(VisualFeatureTypes.Faces);

			var output = await FaceAnalysisService.AnalyzeFaces(req.url, features);

			if (output.IsError)
			{
				return BadRequest();
			}

			return Ok(output.Value);
		}
	}
}
