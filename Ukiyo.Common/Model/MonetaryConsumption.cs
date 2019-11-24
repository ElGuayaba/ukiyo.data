using Microsoft.ML.Data;

namespace Ukiyo.Common.Model
{
	public class MonetaryConsumption
	{
		[ColumnName("Month"), LoadColumn(0)]
		public float Month { get; set; }


		[ColumnName("Electricity"), LoadColumn(1)]
		public string Electricity { get; set; }


		[ColumnName("Heating"), LoadColumn(2)]
		public string Heating { get; set; }


		[ColumnName("Water"), LoadColumn(3)]
		public string Water { get; set; }


		[ColumnName("Price"), LoadColumn(4)]
		public float Price { get; set; }
	}
}
