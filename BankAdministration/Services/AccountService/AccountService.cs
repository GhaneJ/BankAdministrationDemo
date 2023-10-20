namespace BankAdministration.Services;

using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[BindProperties]
public class AccountService : IAccountService
{
    private readonly BankContext _context;
    private readonly ICustomerService _customerService;

    public string Frequency { get; set; }
    public decimal Balance { get; set; }
    public DateTime Created { get; set; }

    public AccountService(BankContext context, ICustomerService customerService)
    {
        _context = context;
        _customerService = customerService;

    }

    public List<Account> GetAll()
    {
        return _context.Accounts.ToList();
    }

    public void Update(Account account)
    {
        _context.SaveChanges();
    }

    public Account GetAccount(int accountId)
    {
        return _context.Accounts.Include(e => e.Transactions).First(e => e.AccountId == accountId);
    }

    public Account CreateAccount(decimal balance, DateTime created, string frequency)
    {
        Account account = new Account
        {
            Balance = Balance,
            Created = DateTime.Now,
            Frequency = "Monthly"
        };
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return account;
    }
}