using Newtonsoft.Json;

namespace Ukiyo.Infrastructure.Contract.Models.FaceAnalysis
{
	public class Face
	{
		[JsonProperty("age", NullValueHandling = NullValueHandling.Ignore)]
		public long? Age { get; set; }

		[JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
		public string Gender { get; set; }

		[JsonProperty("faceRectangle", NullValueHandling = NullValueHandling.Ignore)]
		public FaceRectangle FaceRectangle { get; set; }
	}
}
