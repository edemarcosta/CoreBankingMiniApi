using CoreBankingMiniApi.Data;
using CoreBankingMiniApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreBankingMiniApi.DTOs;

namespace CoreBankingMiniApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly BankingDbContext _context;

    public AccountsController(BankingDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Account>> CreateAccount(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);

        if (customer == null)
            return NotFound("Customer not found.");

        var account = new Account
        {
            CustomerId = customerId,
            AccountNumber = $"ACC-{DateTime.UtcNow:yyyyMMddHHmmss}",
            Balance = 0,
            Status = "Active",
            CreatedAt = DateTime.UtcNow
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        var response = new
        {
            account.Id,
            account.CustomerId,
            account.AccountNumber,
            account.Balance,
            account.Status,
            account.CreatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = account.Id }, response);
    }
    [HttpGet("{accountId}/transactions")]
    public async Task<ActionResult> GetTransactions(int accountId)
    {
        var transactions = await _context.Transactions
            .Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        return Ok(transactions);
    }
    [HttpPost("transfer")]
    public async Task<ActionResult> Transfer(TransferDto transfer)
    {
        if (transfer.Amount <= 0)
            return BadRequest("Invalid amount.");

        if (transfer.FromAccountId == transfer.ToAccountId)
            return BadRequest("Source and destination accounts must be different.");

        var fromAccount = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == transfer.FromAccountId);

        if (fromAccount == null)
            return NotFound("Source account not found.");

        var toAccount = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == transfer.ToAccountId);

        if (toAccount == null)
            return NotFound("Destination account not found.");

        if (fromAccount.Balance < transfer.Amount)
            return BadRequest("Insufficient funds.");

        fromAccount.Balance -= transfer.Amount;
        toAccount.Balance += transfer.Amount;

        var debitTransaction = new BankTransaction
        {
            AccountId = fromAccount.Id,
            Type = "Transfer Debit",
            Amount = transfer.Amount,
            Description = $"Transfer to account {toAccount.AccountNumber}. {transfer.Description}",
            CreatedAt = DateTime.UtcNow
        };

        var creditTransaction = new BankTransaction
        {
            AccountId = toAccount.Id,
            Type = "Transfer Credit",
            Amount = transfer.Amount,
            Description = $"Transfer from account {fromAccount.AccountNumber}. {transfer.Description}",
            CreatedAt = DateTime.UtcNow
        };

        _context.Transactions.Add(debitTransaction);
        _context.Transactions.Add(creditTransaction);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "Transfer completed successfully.",
            FromAccount = new
            {
                fromAccount.Id,
                fromAccount.AccountNumber,
                fromAccount.Balance
            },
            ToAccount = new
            {
                toAccount.Id,
                toAccount.AccountNumber,
                toAccount.Balance
            }
        });
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var account = await _context.Accounts
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (account == null)
            return NotFound("Account not found.");

        var response = new
        {
            account.Id,
            account.CustomerId,
            CustomerName = account.Customer.FullName,
            account.AccountNumber,
            account.Balance,
            account.Status,
            account.CreatedAt
        };

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var accounts = await _context.Accounts
            .Include(a => a.Customer)
            .Select(a => new
            {
                a.Id,
                a.CustomerId,
                CustomerName = a.Customer.FullName,
                a.AccountNumber,
                a.Balance,
                a.Status,
                a.CreatedAt
            })
            .ToListAsync();

        return Ok(accounts);
    }
    [HttpPost("deposit")]
    public async Task<ActionResult> Deposit(int accountId, decimal amount)
    {
        var account = await _context.Accounts.FindAsync(accountId);

        if (account == null)
            return NotFound("Account not found.");

        if (amount <= 0)
            return BadRequest("Invalid amount.");

        account.Balance += amount;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            account.Id,
            account.Balance
        });
    }
    [HttpPost("withdraw")]
    public async Task<ActionResult> Withdraw(int accountId, decimal amount)
    {
        var account = await _context.Accounts.FindAsync(accountId);

        if (account == null)
            return NotFound("Account not found.");

        if (amount <= 0)
            return BadRequest("Invalid amount.");

        if (account.Balance < amount)
            return BadRequest("Insufficient funds.");

        account.Balance -= amount;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            account.Id,
            account.Balance
        });
    }

}