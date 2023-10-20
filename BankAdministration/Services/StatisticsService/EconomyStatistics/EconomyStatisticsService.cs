namespace BankAdministration.Services.StatisticsService.EconomyStatistics;

using BankAdministration.Infrastructure.Paging;
using BankAdministration.Models;

public class EconomyStatisticsService : IEconomyStatisticsService
{
    private readonly BankContext _context;

    public EconomyStatisticsService(BankContext context)
    {
        _context = context;
    }

    public int NumberOfAvailabeAccounts()
    {
        return _context.Accounts.Count();
    }

    public string SumOfAccountBalances()
    {
        return _context.Accounts.Sum(r => r.Balance).ToString();
    }

    public int NumberOfIssuedCards()
    {
        return _context.Cards.Count();
    }

    public int NumberOfLoans()
    {
        return _context.Loans.Count();
    }

    public PagedResult<Customer> ListCustomers(int customerId, string sortColumn, string sortOrder, int page, string searchWord)
    {
        //ProductName, UnitPrice and UnitsInStock
        var query = _context.Customers.Where(p => p.CustomerId == customerId);
        if (!string.IsNullOrEmpty(searchWord))
        {
            query = query.Where(r => r.Givenname.Contains(searchWord) || r.Surname.Contains(searchWord));
        }
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
                query = query.OrderByDescending(p => p.Givenname);
            else
                query = query.OrderBy(p => p.Givenname);
        }
        if (sortColumn == "Surname")
        {
            if (sortOrder == "desc")
                query = query.OrderByDescending(p => p.Surname);
            else
                query = query.OrderBy(p => p.Surname);
        }
        if (sortColumn == "Streetaddress")
        {
            if (sortOrder == "desc")
                query = query.OrderByDescending(p => p.Streetaddress);
            else
                query = query.OrderBy(p => p.Streetaddress);
        }
        return query.GetPaged(page, 5); //5 is the pagesize
    }
}
