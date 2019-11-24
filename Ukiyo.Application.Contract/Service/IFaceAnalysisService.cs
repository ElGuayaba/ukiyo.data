using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using OperationResult;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ukiyo.Common.ErrorHandling;

namespace Ukiyo.Application.Contract.Service
{
	public interface IFaceAnalysisService
    {
		Task<Result<ImageAnalysis, Error>> AnalyzeFaces(string imageUrl, List<VisualFeatureTypes> features);

	}
}