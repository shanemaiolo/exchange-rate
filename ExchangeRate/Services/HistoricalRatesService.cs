using ExchangeRate.Clients;
using ExchangeRate.Models;

namespace ExchangeRate.Services
{
	public interface IHistoricalRatesService
    {
		HistoricalRatesModel GetHistoricalRates(List<DateTimeOffset> dates, string baseCurrencyCode, string targetCurrencyCode);
    }

	public class HistoricalRatesService : IHistoricalRatesService
	{
		private readonly ILogger<HistoricalRatesService> _logger;
		private readonly IExchangeRateHostClient _exchangeRateHostClient;

        public HistoricalRatesService(
			ILogger<HistoricalRatesService> logger,
			IExchangeRateHostClient exchangeRateHostClient)
		{
			_logger = logger;
			_exchangeRateHostClient = exchangeRateHostClient;
		}

        public HistoricalRatesModel GetHistoricalRates(
            List<DateTimeOffset> dates,
            string baseCurrencyCode,
            string targetCurrencyCode)
        {
            _logger.LogInformation("Dates: {dates}, Base Currency Code: {baseCurrencyCode}, Target Currency Code: {targetCurrencyCode}",
                dates, baseCurrencyCode, targetCurrencyCode);

            var tasks = new List<Task>();
            var rates = new List<RatesModel>();

            foreach (var date in dates)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var historicalRate = await _exchangeRateHostClient.GetHistoricalRates(date.ToString("yyyy-MM-dd"), baseCurrencyCode, targetCurrencyCode);

                    if (historicalRate != null && historicalRate.Success)
                    {
                        var rate = historicalRate.Rates[targetCurrencyCode];
                        rates.Add(new RatesModel(historicalRate.Date, rate));
                    }
                }));
            };

            Task t = Task.WhenAll(tasks);

            try
            {
                t.Wait();
            }
            catch { }

            if (t.Status != TaskStatus.RanToCompletion)
            {
                throw new Exception("Failed requesting Historical Rates");
            }

            return ProcessClientRates(rates);
        }

        private HistoricalRatesModel ProcessClientRates(List<RatesModel> rates)
		{
			List<RatesModel> sortedRates = rates.OrderBy(x => x.Rate).ToList();

			var min = sortedRates.First();
			var max = sortedRates.Last();
			var avg = sortedRates.Average(x => x.Rate);

			return new HistoricalRatesModel(min, max, avg);
		}
	}
}
