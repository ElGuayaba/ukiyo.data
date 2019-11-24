using Newtonsoft.Json;

namespace Ukiyo.Infrastructure.Contract.Models.FaceAnalysis
{
	public class FaceRectangle
	{
		[JsonProperty("left", NullValueHandling = NullValueHandling.Ignore)]
		public long? Left { get; set; }

		[JsonProperty("top", NullValueHandling = NullValueHandling.Ignore)]
		public long? Top { get; set; }

		[JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
		public long? Width { get; set; }

		[JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
		public long? Height { get; set; }
	}
}
