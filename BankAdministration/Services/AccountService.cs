using System.Collections.Immutable;
using System.Linq;
using BankAdministration.Data;
using BankAdministration.Infrastructure.Paging;
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
        return _context.Accounts.Where(e => e.AccountId == id).First(e => e.AccountId == id);
    }



    public PagedResult<Disposition> ListAccounts(int accountId, string sortColumn, string sortOrder, int page, string searchWord)
    {
        var query = _context.Dispositions.AsQueryable();

        query = query.Include(r=>r.Customers);
        if (!string.IsNullOrEmpty(searchWord))
        {
            query = query.Where(r => r.AccountId.ToString().Equals(searchWord));

        }
        //By default, the result is sorted by Givenname and is ascending
        if (string.IsNullOrEmpty(sortColumn))
        {
            sortColumn = "Givenname";
        }
        if (string.IsNullOrEmpty(sortOrder))
        {
            sortOrder = "asc";
        }
        if (sortColumn == "Givenname")
        {
            if (sortOrder == "desc")
                query = query.OrderByDescending(e => e.Customers.Givenname);

            else
                query = query.OrderBy(e => e.Customers.Givenname);
        }
        if (sortColumn == "Streetaddress")
        {
            if (sortOrder == "desc")
                query = query.OrderByDescending(e => e.Customers.Streetaddress);
            else
                query = query.OrderBy(e => e.Customers.Streetaddress);
        }
        if (sortColumn == "City")
        {
            if (sortOrder == "desc")
                query = query.OrderByDescending(e => e.Customers.Streetaddress);
            else
                query = query.OrderBy(e => e.Customers.Streetaddress);
        }
        if (sortColumn == "AccountId")
        {
            if (sortOrder == "desc")
                query = query.OrderByDescending(e => e.AccountId);
            else
                query = query.OrderBy(e => e.AccountId);

        }
        return query.GetPaged(page, 25); //5 is the pagesize
    }
}