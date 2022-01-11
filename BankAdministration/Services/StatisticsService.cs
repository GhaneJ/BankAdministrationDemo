using BankAdministration.Infrastructure.Paging;
using BankAdministration.Models;
using System.Linq;

namespace BankAdministration.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly BankContext _context;

        public StatisticsService(BankContext context)
        {
            _context = context;
        }

        public string activeCustomers()
        {
            return _context.Customers.Where(r => r.Active == true).Count().ToString();
        }

        public string availabAccounts()
        {
            return _context.Accounts.Count().ToString();
        }

        public string sumOfAccountBalances()
        {
            return _context.Accounts.Sum(r => r.Balance).ToString();
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
}







//availableCustomers = _context.Customers.Where(r=>r.Active==true).Count().ToString();
//availableAccounts = _context.Accounts.Count().ToString();
//sumOfBalances = _context.Accounts.Sum(r => r.Balance).ToString();

//var query = _context.Accounts.Select(r => new IndexViewModel
//{
//    Id = r.AccountId,
//    Balance = r.Balance.ToString(),
//    Frequency = r.Frequency,
//    Created = r.Created
//}).Where(e => string.IsNullOrEmpty(q) || e.Balance.Contains(q));