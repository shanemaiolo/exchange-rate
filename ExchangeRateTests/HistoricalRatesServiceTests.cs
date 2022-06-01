using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExchangeRate.Clients;
using ExchangeRate.Clients.ExchangeRateHost.Models;
using ExchangeRate.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ExchangeRateTests;

public class HistoricalRatesServiceTests
{
    [Fact]
    public async Task GetHistoricalRatesSuccessAsync()
    {
        var utcNow = DateTimeOffset.UtcNow;
        var today = new DateTimeOffset(utcNow.Year, utcNow.Month, utcNow.Day, 0, 0, 0, new TimeSpan(0));
        var yesterday = today.AddDays(-1);

        var mockLogger = new Mock<ILogger<HistoricalRatesService>>();

        var mockClient = new Mock<IExchangeRateHostClient>();

        mockClient
            .Setup(x => x.GetHistoricalRates(today.ToString("yyyy-MM-dd"), "SEK", "NOK"))
            .ReturnsAsync(new HistoricalRatesClientModel()
            {
                Success = true,
                Historical = true,
                Date = today,
                Rates = new Dictionary<string, double>()
                {
                    { "NOK", 1.2345678},
                    { "ABC", 1.9876543},
                }
            });

        mockClient
            .Setup(x => x.GetHistoricalRates(yesterday.ToString("yyyy-MM-dd"), "SEK", "NOK"))
            .ReturnsAsync(new HistoricalRatesClientModel()
            {
                Success = true,
                Historical = true,
                Date = yesterday,
                Rates = new Dictionary<string, double>()
                {
                    { "NOK", 2.3456789},
                    { "XYZ", 2.9876543},
                }
            });

        var service = new HistoricalRatesService(mockLogger.Object, mockClient.Object);

        var result = await service.GetHistoricalRatesAsync(new List<DateTimeOffset>{ yesterday, today }, "SEK", "NOK");

        Assert.Equal(1.2345678, result.MinRate.Rate);
        Assert.Equal(today, result.MinRate.Date);
        Assert.Equal(2.3456789, result.MaxRate.Rate);
        Assert.Equal(yesterday, result.MaxRate.Date);
        Assert.Equal(1.79012335, result.AvgRate);
    }
}
