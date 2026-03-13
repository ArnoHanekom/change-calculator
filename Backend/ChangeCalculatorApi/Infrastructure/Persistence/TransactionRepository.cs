using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class TransactionRepository(TransactionDbContext context) : ITransactionRepository
{
    public async Task AddAsync(Transaction trans, CancellationToken cancellationToken = default)
    {
        context.Transactions.Add(trans);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Transactions.OrderByDescending(t => t.TransactionDate)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}