using System;

namespace Ukiyo.Common.Model
{
	public class ConsumptionDto
	{
		public DateTime Date { get; set; }
		public double Electricity { get; set; }
		public double Heating { get; set; }
		public double Water { get; set; }
	}
}
