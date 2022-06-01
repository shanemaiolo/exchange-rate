# Historical Exchange Rate Assignment

## Description

This project is for the coding assignment to implement a REST service that returns historical exchange rate information.

The source API and associated documentation, [https://exchangerate.host/](https://exchangerate.host/)

## Requirements

Implement an endpoint that accepts:

* A set of dates

* A base currency

* A target currency

Which returns:

* The minimum exchange rate (and it's associated date) during the period

* The maximum exchange rate (and it's associated date) during the period

* The average exchange rate during the period

## Validation 

Using the following sample input provided:

* Dates: 2018-02-01, 2018-02-15, 2018-03-01
* Currency SEK->NOK

The expected output should be:

* A min rate of 0.952702 on 2018-03-01
* A max rate of 0.979845 on 2018-02-15
* An average rate of 0.9702316666666667

## Available Commands

### `dotnet restore`

### `dotnet build`

### `dotnet run`

*(from within the `ExchangeRate` directory)*

### `dotnet run --project ExchangeRate`

*(from the root directory)*

#### Ports
HTTPS port `7654`

HTTP port `5432`

#### Notes

Running the solution from Visual Studio will load the API Documentation in the browser, [https://localhost:7654/swagger/index.html](https://localhost:7654/swagger/index.html)

The API endpoint that will accept the Historical Rates POST request, [https://localhost:7654/api/v1/historical-rates](https://localhost:7654/api/v1/historical-rates)

### `dotnet test`

*(from the root directory will run all discoverable tests)*

## Routes

### POST `/api/v1/historical-rates`

Request Body

```JSON
{
  "dates": [
    "2018-02-01", "2018-02-15", "2018-03-01"
  ],
  "baseCurrencyCode": "SEK",
  "targetCurrencyCode": "NOK"
}
```

Response Object

```JSON
{
  "minRate": {
    "date": "2018-03-01T00:00:00",
    "rate": 0.952702
  },
  "maxRate": {
    "date": "2018-02-15T00:00:00",
    "rate": 0.979845
  },
  "avgRate": 0.9702316666666667
}
```

## Assumptions and Limitations

The input dates will cause an error if they cannot be converted into type `DateTimeOffset` used by the `HistoricalRatesRequestModel`.

Any time component of the `DateTimeOffset` is ignored.

Currently a single client is defined in scope but this could refactored to iterate over the HttpClients settings to dynamically add each configured client from the settings.
