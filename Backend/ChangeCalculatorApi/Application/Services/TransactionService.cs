using Application.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using System.Text.Json;

namespace Application.Services;

public class TransactionService(ITransactionRepository transactionRepository, ICurrencyProvider currencyProvider)
{
    public async Task<IEnumerable<Transaction>> GetAllTransactionHistoryAsync(CancellationToken cancellationToken = default)
    {
        return await transactionRepository.GetAllAsync(cancellationToken) ?? [];
    }
    
    public async Task<Transaction> CreateTransactionAsync(TransactionRequestDto request, CancellationToken cancellationToken)
    {
        var currencies = await currencyProvider.GetCurrenciesAsync(cancellationToken);
        if (!currencies.ContainsKey(request.CurrencyCode)) throw new ArgumentException($"Currency {request.CurrencyCode} not supported.");
        if (request.AmountPaid < request.AmountOwed) throw new InvalidOperationException("Amount paid is less than the amount owed.");

        var denominations = currencies[request.CurrencyCode];
        var changeDue = Math.Round(request.AmountPaid - request.AmountOwed, 2);

        var trans = new Transaction()
        {
            TransactionDate = DateTime.UtcNow,
            CurrencyCode = request.CurrencyCode,
            AmountOwed = request.AmountOwed,
            AmountPaid = request.AmountPaid,
            ChangeDue = changeDue,
            ChangeBreakdown = JsonSerializer.Serialize(GenerateChangeBreakdown(changeDue, denominations))
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