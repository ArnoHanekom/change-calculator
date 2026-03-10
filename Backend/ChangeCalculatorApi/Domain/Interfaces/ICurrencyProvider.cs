namespace Domain.Interfaces;

public interface ICurrencyProvider
{
    Task<IReadOnlyDictionary<string, List<decimal>>> GetCurrenciesAsync(CancellationToken cancellationToken);
}