namespace Application.Dtos;

public record TransactionRequestDto(
    string CurrencyCode,
    decimal AmountOwed,
    decimal AmountPaid
);