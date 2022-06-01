using System;

namespace ExchangeRate.Controllers.Models
{
	public class HistoricalRatesRequestModel
	{
		public List<DateTimeOffset> Dates { get; }
		public string BaseCurrencyCode { get; }
		public string TargetCurrencyCode { get; }

		public HistoricalRatesRequestModel(
			List<DateTimeOffset> dates,
			string baseCurrencyCode,
			string targetCurrencyCode)
		{
			Dates = dates;
			BaseCurrencyCode = baseCurrencyCode;
			TargetCurrencyCode = targetCurrencyCode;
		}
	}
}
