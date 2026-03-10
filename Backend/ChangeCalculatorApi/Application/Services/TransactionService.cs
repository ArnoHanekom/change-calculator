using Domain.Entities;
using Domain.Interfaces;
using System.Text.Json;

namespace Application.Services;

public class TransactionService(ITransactionRepository transactionRepository, ICurrencyProvider currencyProvider)
{
    public async Task<IEnumerable<Transaction>> GetAllTransactionHistoryAsync(CancellationToken cancellationToken = default)
    {
        return await transactionRepository.GetAllAsync() ?? [];
    }
    
    public async Task<Transaction> CreateTransactionAsync(string currencyCode, decimal amountOwed, decimal amountPaid, CancellationToken cancellationToken)
    {
        var currencies = await currencyProvider.GetCurrenciesAsync(cancellationToken);
        if (!currencies.ContainsKey(currencyCode)) throw new ArgumentException($"Currency {currencyCode} not supported.");
        if (amountPaid < amountOwed) throw new InvalidOperationException("Amount paid is less than the amount owed.");

        var denominations = currencies[currencyCode];
        var due = Math.Round(amountPaid - amountOwed, 2);

        var trans = new Transaction()
        {
            TransactionDate = DateTime.UtcNow,
            CurrencyCode = currencyCode,
            AmountOwed = amountOwed,
            AmountPaid = amountPaid,
            ChangeDue = due,
            ChangeBreakdown = JsonSerializer.Serialize(GenerateChangeBreakdown(due, denominations))
        };

        await transactionRepository.AddAsync(trans, cancellationToken);

        return trans;
    }
    private static Dictionary<decimal, int> GenerateChangeBreakdown(decimal change, List<decimal> denominations)
    {
        var breakdown = new Dictionary<decimal, int>();
        foreach (var coin in denominations.OrderByDescending(d => d))
        {
            int count = (int)(change / coin);
            if (count > 0)
            {
                breakdown[coin] = count;
                change = Math.Round(change - (count * coin), 2);
            }
        }
        return breakdown;
    }
}