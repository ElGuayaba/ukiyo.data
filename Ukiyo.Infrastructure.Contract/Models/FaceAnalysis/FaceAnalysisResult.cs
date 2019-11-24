namespace Ukiyo.Infrastructure.Contract.Models.FaceAnalysis
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;
	using System;
	using System.Collections.Generic;

	using System.Globalization;

	public partial class FaceAnalysisResult
	{
		[JsonProperty("faces", NullValueHandling = NullValueHandling.Ignore)]
		public List<Face> Faces { get; set; }

		[JsonProperty("requestId", NullValueHandling = NullValueHandling.Ignore)]
		public Guid? RequestId { get; set; }

		[JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
		public Metadata Metadata { get; set; }
	}

	public partial class FaceAnalysisResult
	{
		public static FaceAnalysisResult FromJson(string json) => JsonConvert.DeserializeObject<FaceAnalysisResult>(json, Converter.Settings);
	}

	public static class Serialize
	{
		public static string ToJson(this FaceAnalysisResult self) => JsonConvert.SerializeObject(self, Converter.Settings);
	}

	internal static class Converter
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters =
			{
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};
	}
}
