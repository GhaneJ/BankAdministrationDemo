using System.Collections.Immutable;
using BankAdministration.Data;
using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.EntityFrameworkCore;

namespace BankAdministration.Services;

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
        return _context.Accounts.Where(e=>e.AccountId == id).Include(e=>e.Dispositions).ThenInclude(e=>e.Customer).First(e => e.AccountId == id);
    }
}