using System;

namespace ExchangeRate.Models
{
	public class RatesModel
	{
		public DateTimeOffset Date { get; }
		public double Rate { get; }

		public RatesModel(
			DateTimeOffset date,
			double rate)
		{
			Date = date;
			Rate = rate;
		}
	}
}
