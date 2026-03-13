namespace Application.Dtos;
public record TransactionResponseDto(
    int Id,
    DateTime TransactionDate,
    string CurrencyCode,
    decimal AmountOwed,
    decimal AmountPaid,
    decimal ChangeDue,
    string ChangeBreakdown
);