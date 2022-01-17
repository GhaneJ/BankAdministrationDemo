using System.Collections.Immutable;
using BankAdministration.Data;
using BankAdministration.Models;
using BankAdministration.Services;

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

    public Account GetAccount(int uniqueId)
    {
        return _context.Accounts.First(e => e.AccountId == uniqueId);
    }
}