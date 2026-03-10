using Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Config;

public class CurrencyProvider(IConfiguration config) : ICurrencyProvider
{
    public Task<IReadOnlyDictionary<string, List<decimal>>> GetCurrenciesAsync(CancellationToken cancellationToken)
    {
        var currencies = config
            .GetSection("Currencies")
            .GetChildren()
            .ToDictionary(c => c.Key, c => c.Get<List<decimal>>() ?? []);
        return Task.FromResult((IReadOnlyDictionary<string, List<decimal>>)currencies);
    }
}