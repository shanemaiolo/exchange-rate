using ExchangeRate.Controllers.Models;
using ExchangeRate.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRate.Controllers
{
    [Route("api/v1/historical-rates")]
    [ApiController]
    public class HistoricalRatesController : ControllerBase
    {
        private readonly ILogger<HistoricalRatesController> _logger;
        private readonly IHistoricalRatesService _historicalRatesService;

        public HistoricalRatesController(
            ILogger<HistoricalRatesController> logger,
            IHistoricalRatesService historicalRatesService)
        {
            _logger = logger;
            _historicalRatesService = historicalRatesService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] HistoricalRatesRequestModel request)
        {
            _logger.LogInformation(
                "GetHistoricalRates request received for Dates: {Dates}, Base Currency Code: {BaseCurrencyCode}, Target Currency Code: {TargetCurrencyCode}",
                request.Dates,
                request.BaseCurrencyCode,
                request.TargetCurrencyCode);

            var response = await _historicalRatesService.GetHistoricalRatesAsync(request.Dates, request.BaseCurrencyCode, request.TargetCurrencyCode);

            return Ok(response);
        }
    }
}
