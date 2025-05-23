using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ExchangeRateService
{
    private readonly HttpClient _client;
    private readonly string _apiUrl;

    public ExchangeRateService(string apiUrl)
    {
        _client = new HttpClient();
        _apiUrl = apiUrl;
    }

    public async Task<decimal> GetCurrencyToRubRateAsync(string currency)
    {
        var response = await _client.GetAsync(_apiUrl + currency);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<ExchangeRateResponse>(json);
        return data.Rate;
    }

    private class ExchangeRateResponse
    {
        public string Currency { get; set; }
        public decimal Rate { get; set; }
    }
}