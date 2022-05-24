using System;
namespace ExchangeRate.Models
{
	public class HistoricalRatesModel
	{
		public double MinRate { get; }
		public double MaxRate { get; }
		public double AvgRate { get; }

		public HistoricalRatesModel(
			double minRate,
			double maxRate,
			double avgRate)
		{
			MinRate = minRate;
			MaxRate = maxRate;
			AvgRate = avgRate;
		}
	}
}

