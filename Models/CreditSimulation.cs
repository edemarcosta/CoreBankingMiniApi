namespace CoreBankingMiniApi.Models;

public class CreditSimulation
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public decimal RequestedAmount { get; set; }

    public int Installments { get; set; }

    public decimal InterestRate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal MonthlyPayment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
