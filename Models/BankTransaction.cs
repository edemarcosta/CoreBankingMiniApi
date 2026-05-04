using CoreBankingMiniApi.Models;

public class BankTransaction
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public Account Account { get; set; } = null!;

    public string Type { get; set; } = string.Empty;
    // Deposit, Withdraw, Transfer

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
