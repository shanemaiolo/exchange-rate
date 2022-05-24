using System;
using ExchangeRate.Models;

namespace ExchangeRate.Services
{
	public interface IHistoricalRatesService
    {
        HistoricalRatesModel GetHistoricalRates(IList<DateTimeOffset> dates, string baseCurrencyCode, string targetCurrencyCode);
    }

	public class HistoricalRatesService : IHistoricalRatesService
	{
		private readonly ILogger<HistoricalRatesService> _logger;

        public HistoricalRatesService(ILogger<HistoricalRatesService> logger)
		{
			_logger = logger;
		}

		public HistoricalRatesModel GetHistoricalRates(
			IList<DateTimeOffset> dates,
			string baseCurrencyCode,
			string targetCurrencyCode)
		{
			_logger.LogInformation("Dates {dates}, Base Currency Code {baseCurrencyCode}, Target Currency Code {targetCurrencyCode}",
				dates, baseCurrencyCode, targetCurrencyCode);

			return new HistoricalRatesModel(1.2345678, 2.3456789, 1.9876543);
		}
	}
}
