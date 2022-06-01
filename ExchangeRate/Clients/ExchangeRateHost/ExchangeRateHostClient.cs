using System;
using ExchangeRate.Clients.ExchangeRateHost.Models;
using ExchangeRate.Constants;
using Newtonsoft.Json;

namespace ExchangeRate.Clients
{
    public interface IExchangeRateHostClient
    {
        Task<HistoricalRatesClientModel> GetHistoricalRates(
            string date,
            string baseCurrencyCode,
            string targetCurrencyCode);
    }

    public class ExchangeRateHostClient: IExchangeRateHostClient
    {
        private readonly ILogger<ExchangeRateHostClient> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

		public ExchangeRateHostClient(
            ILogger<ExchangeRateHostClient> logger,
            IHttpClientFactory httpClientFactory)
		{
            _logger = logger;
			_httpClientFactory = httpClientFactory;
		}

        /// <summary>
        /// Get the Historical Rates for a given Date, Base and Target currency
        /// </summary>
        /// <param name="date"></param>
        /// <param name="baseCurrencyCode"></param>
        /// <param name="targetCurrencyCode"></param>
        /// <returns></returns>
		public async Task<HistoricalRatesClientModel> GetHistoricalRates(
            string date,
            string baseCurrencyCode,
            string targetCurrencyCode)
        {
            // Sample query string format, 2018-02-15?base=ABC&symbols=XYZ
            var request = new HttpRequestMessage(HttpMethod.Get, $"{date}?base={baseCurrencyCode}&symbols={targetCurrencyCode}");

			var client = _httpClientFactory.CreateClient(HttpClientConstants.ExchangeRateHost);

			var response = await client.SendAsync(request);

            HistoricalRatesClientModel rates = new HistoricalRatesClientModel();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var result = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(result))
                    {
                        rates = JsonConvert.DeserializeObject<HistoricalRatesClientModel>(result)!;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting historical rates");
                }
            }

            return rates;
        }
	}
}
