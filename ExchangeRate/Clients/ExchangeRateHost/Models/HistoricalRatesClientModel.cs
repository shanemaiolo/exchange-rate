using System;

namespace ExchangeRate.Clients.ExchangeRateHost.Models
{
    /// <summary>
    ///
    /// Sample JSON returned by Historical Rates request (truncated ... where appropriate),
    /// 
    ///  {
    ///    "motd": {
    ///        "msg": "If you or your company use this project or like what we doing...",
    ///        "url": "https://exchangerate.host/#/donate"
    ///    },
    ///    "success": true,
    ///    "historical": true,
    ///    "base": "EUR",
    ///    "date": "2018-02-15",
    ///    "rates": {
    ///        "USD": 1.2468,
    ///        "...": 1.2345
    ///    }
    ///  }
    /// </summary>

    public class HistoricalRatesClientModel
	{
        public bool Success { get; set; } = false;
        public bool Historical { get; set; }
        public string Base { get; set; } = null!; 
        public DateTimeOffset Date { get; set; }
        public IDictionary<string, double> Rates { get; set; } = default!;
    }
}
