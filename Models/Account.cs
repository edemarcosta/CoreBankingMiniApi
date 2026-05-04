namespace CoreBankingMiniApi.Models;

public class Account
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public string AccountNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public string Status { get; set; } = "Active";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<BankTransaction> Transactions { get; set; } = new();
}