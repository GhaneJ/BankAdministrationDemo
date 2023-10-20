namespace BankAdministration.Services;

using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc;

[BindProperties]
public class TransactionService : ITransactionService
{
    private readonly BankContext _context;
    private readonly IAccountService _accountService;
    public Account AccountNavigation { get; set; }

    public TransactionService(BankContext context, IAccountService accountService)
    {
        _context = context;
        _accountService = accountService;
    }
    public List<Transaction> GetAll()
    {
        return _context.Transactions.ToList();
    }

    public void Update(Transaction transaction)
    {
        _context.SaveChanges();
    }

    public Transaction CreateTransactionForTransfer(int accountId, int accountNo, decimal amount, string operation, string bank, string symbol, AccountType type)
    {
        var account = _accountService.GetAccount(accountId);
        AccountNavigation = _context.Accounts.First(e => e.AccountId == accountId);
        Transaction transaction = new Transaction();
        {
            if (accountNo == accountId)
            {
                transaction.Amount = -amount;
            }
            else
            {
                transaction.Amount = amount;
            }
            transaction.Symbol = symbol;
            transaction.Date = DateTime.Now;
            transaction.Type = type.ToString();
            transaction.Operation = operation;
            transaction.Bank = bank;
            transaction.Balance = account.Balance + transaction.Amount;
            transaction.Account = accountNo.ToString();
            transaction.AccountNavigation = AccountNavigation;
            account.Balance = transaction.Balance;
            _accountService.Update(account);

        };
        _context.Transactions.Add(transaction);
        return transaction;
    }
    public Transaction CreateTransactionForWithdraw(int accountId, decimal amount, decimal balance, AccountType type)
    {
        var account = _accountService.GetAccount(accountId);
        AccountNavigation = _context.Accounts.First(e => e.AccountId == accountId);
        Transaction transaction = new Transaction();
        {
            transaction.Amount = -amount;
            transaction.Symbol = "Cash uttag";
            transaction.Bank = "Egen Bank";
            transaction.Account = "Eget konto";
            transaction.Date = DateTime.Now;
            transaction.Operation = "Uttag";
            transaction.Type = type.ToString();
            transaction.Balance = account.Balance + transaction.Amount;
            transaction.AccountNavigation = AccountNavigation;
        };
        account.Balance = transaction.Balance;
        _accountService.Update(account);
        _context.Transactions.Add(transaction);
        return transaction;
    }
    public Transaction CreateTransactionForDeposit(int accountId, decimal amount, string comment, AccountType type)
    {
        var account = _accountService.GetAccount(accountId);
        AccountNavigation = _context.Accounts.First(e => e.AccountId == accountId);
        Transaction transaction = new Transaction();
        {
            transaction.Amount = amount;
            transaction.Symbol = comment;
            transaction.Bank = "Egen Bank";
            transaction.Account = "Eget konto";
            transaction.Date = DateTime.Now;
            transaction.Type = type.ToString();
            transaction.Operation = "Insättning";
            transaction.Balance = account.Balance + transaction.Amount;
            transaction.AccountNavigation = AccountNavigation;
        };
        account.Balance = transaction.Balance;
        _accountService.Update(account);
        _context.Transactions.Add(transaction);
        return transaction;
    }
}
