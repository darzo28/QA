using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var apiUrl = "http://localhost:4545/api/exchange-rate";
        var service = new ExchangeRateService(apiUrl);

        try
        {
            decimal rate = await service.GetCurrencyToRubRateAsync("usd");
            Console.WriteLine($"Курс USD к RUB: {rate}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении курса: {ex.Message}");
        }
    }
}