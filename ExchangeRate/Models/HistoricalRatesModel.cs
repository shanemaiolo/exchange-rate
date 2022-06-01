using System;

namespace ExchangeRate.Models
{
	public class HistoricalRatesModel
	{
		public RatesModel MinRate { get; }
		public RatesModel MaxRate { get; }
		public double AvgRate { get; }

		public HistoricalRatesModel(
			RatesModel minRate,
			RatesModel maxRate,
			double avgRate)
		{
			MinRate = minRate;
			MaxRate = maxRate;
			AvgRate = avgRate;
		}
	}
}
