using System;
using System.Collections.Generic;
using ExchangeRate.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ExchangeRateTests;

public class HistoricalRatesServiceTests
{
    [Fact]
    public void GetHistoricalRatesSuccess()
    {
        var mockLogger = new Mock<ILogger<HistoricalRatesService>>();

        var service = new HistoricalRatesService(mockLogger.Object);

        var result = service.GetHistoricalRates(new List<DateTimeOffset>{ DateTimeOffset.UtcNow }, "SEK", "NOK");

        Assert.Equal(1.2345678, result.MinRate);
        Assert.Equal(2.3456789, result.MaxRate);
        Assert.Equal(1.9876543, result.AvgRate);
    }
}
