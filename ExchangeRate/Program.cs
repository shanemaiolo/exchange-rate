using ExchangeRate.Clients;
using ExchangeRate.Constants;
using ExchangeRate.Models;
using ExchangeRate.Services;

//Configuration
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddScoped<IHistoricalRatesService, HistoricalRatesService>();

// Clients
builder.Services.AddScoped<IExchangeRateHostClient, ExchangeRateHostClient>();

var exchangeRateHostSettings = config
    .GetRequiredSection($"HttpClients:{HttpClientConstants.ExchangeRateHost}")
    .Get<HttpClientSettingsModel>();

builder.Services.AddHttpClient(HttpClientConstants.ExchangeRateHost, x =>
{
    x.BaseAddress = new Uri(exchangeRateHostSettings.BaseAddress);
    x.DefaultRequestHeaders.Add("Accept", "application/json");
});

// API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
