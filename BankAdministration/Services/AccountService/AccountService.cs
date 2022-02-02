using System.Collections.Immutable;
using System.Linq;
using BankAdministration.Data;
using BankAdministration.Infrastructure.Paging;
using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAdministration.Services;
[BindProperties]
public class AccountService : IAccountService
{
    
    private readonly BankContext _context;

    public AccountService(BankContext context)
    {
        _context = context;
    }
    public List<Account> GetAll()
    {
        return _context.Accounts.ToList();
    }
    

    public void Update(Account person)
    {
        _context.SaveChanges();
    }
    
    public Account GetAccount(int id)
    {
        return _context.Accounts.Include(e=>e.Transactions).First(e => e.AccountId == id);
    }

    public Account CreateAccount()
    {
        //AccountNavigation = _context.Accounts.First(e => e.AccountId == accountNo);
        Account account = new Account();
        {
            
        };
        _context.Accounts.Add(account);
        return account;
    }
}