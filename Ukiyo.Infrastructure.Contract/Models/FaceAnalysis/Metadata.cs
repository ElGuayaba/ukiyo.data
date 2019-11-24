using Newtonsoft.Json;

namespace Ukiyo.Infrastructure.Contract.Models
{
	public partial class Metadata
	{
		[JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
		public long? Width { get; set; }

		[JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
		public long? Height { get; set; }

		[JsonProperty("format", NullValueHandling = NullValueHandling.Ignore)]
		public string Format { get; set; }
	}
}
