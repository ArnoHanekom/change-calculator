using Application.Dtos;
using Domain.Entities;

namespace Application.Services;

public static class TransactionMapper
{
    public static TransactionResponseDto ToResponseDto(Transaction t) =>
        new(
            t.Id,
            t.TransactionDate,
            t.CurrencyCode,
            t.AmountOwed,
            t.AmountPaid,
            t.ChangeDue,
            t.ChangeBreakdown
        );
}