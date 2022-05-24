using System;

namespace ExchangeRate.Controllers.Models
{
	public class HistoricalRatesRequestModel
	{
		public IList<DateTimeOffset> Dates { get; }
		public string BaseCurrencyCode { get; }
		public string TargetCurrencyCode { get; }

		public HistoricalRatesRequestModel(
			IList<DateTimeOffset> dates,
			string baseCurrencyCode,
			string targetCurrencyCode)
		{
			Dates = dates;
			BaseCurrencyCode = baseCurrencyCode;
			TargetCurrencyCode = targetCurrencyCode;
		}
	}
}
