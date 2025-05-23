using System.Threading.Tasks;
using Xunit;

namespace NestCurrencyClient
{
    public class ExchangeRateTests
    {
        private static readonly string MOCK_URL = "http://localhost:4545/api/exchange-rate/";

        private readonly ExchangeRateService _mockService;

        public ExchangeRateTests()
        {
            _mockService = new(MOCK_URL);
        }

        [Fact]
        public async Task Getting_USD_to_RUB_rate()
        {
            const string currency = "usd";

            decimal rate = await _mockService.GetCurrencyToRubRateAsync(currency);

            Assert.Equal(79.50m, rate);
        }

        [Fact]
        public async Task Getting_UAH_to_RUB_rate()
        {
            const string currency = "cny";

            decimal rate = await _mockService.GetCurrencyToRubRateAsync(currency);

            Assert.Equal(11.04m, rate);
        }

        [Fact]
        public async Task Getting_exchange_rate_with_bad_currency()
        {
            const string currency = "dfsdf";

            await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _mockService.GetCurrencyToRubRateAsync(currency));
        }
    }
}