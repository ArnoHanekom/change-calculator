namespace Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public decimal AmountOwed { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal ChangeDue { get; set; }
    public string ChangeBreakdown { get; set; } = string.Empty;
}