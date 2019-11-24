using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OperationResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ukiyo.Application.Contract.Service;
using Ukiyo.Common.ErrorHandling;
using static OperationResult.Helpers;
using static Ukiyo.Common.ErrorHandling.Helpers;

namespace Ukiyo.Application.Implementation.Service
{
	public class FaceAnalysisService : IFaceAnalysisService
    {
        protected readonly ILogger<FaceAnalysisService> Logger;
		protected readonly IComputerVisionClient ImageAnalysisClient;


		public FaceAnalysisService(
			ILogger<FaceAnalysisService> logger,
			IComputerVisionClient imageAnalysisClient,
			IConfiguration configuration)
        {
            Logger = logger;
			ImageAnalysisClient = imageAnalysisClient;
		}

        public async Task<Result<ImageAnalysis, Error>> AnalyzeFaces(string imageUrl, List<VisualFeatureTypes> features)
        {
			if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
			{
				return Error(UnknownError("Badly formed url"));
			}

			var analysis = await ImageAnalysisClient.AnalyzeImageAsync(imageUrl, features);

			Logger.LogWarning("Response: {@analysis}", analysis);

			return Ok(analysis);
		}
    }
}