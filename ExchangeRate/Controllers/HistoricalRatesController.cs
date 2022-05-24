using ExchangeRate.Controllers.Models;
using ExchangeRate.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRate.Controllers
{
    [Route("api/v1/[controller]")]
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

        // POST: api/HistoricalRates
        [HttpPost]
        public IActionResult Post([FromBody] HistoricalRatesRequestModel request)
        {
            var response = _historicalRatesService.GetHistoricalRates(request.Dates, request.BaseCurrencyCode, request.TargetCurrencyCode);

            return Ok(response);
        }
    }
}
