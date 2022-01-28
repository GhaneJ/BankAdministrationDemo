using BankAdministration.Infrastructure.Paging;
using BankAdministration.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BankAdministration.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankContext _context;
        public CustomerService(BankContext context)
        {
            _context = context;
        }
        public PagedResult<Disposition> ListCustomers(int customerId, string sortColumn, string sortOrder, int page, string searchWord)
        {
            //Asquerable() converts the variable to a queryable IEnumerable(Without it, no data was being loaded)
            //var query = _context.Customers.AsQueryable().Where(r => r.CustomerId == customerId);

            var query = _context.Dispositions.AsQueryable();

            query = query.Include(r => r.Customers);
            query = query.Include(r => r.Account);

            if (!string.IsNullOrEmpty(searchWord))
            {
                query = query.Where(r => r.CustomerId.ToString().Equals(searchWord)
                || r.Customers.Givenname.Equals(searchWord) || r.Customers.City.Equals(searchWord));

                //query = query.Where(r => r.Customers.Givenname.Equals(searchWord) || r.Customers.City.Equals(searchWord));

            }
            //By default, the result is sorted by Givenname and is ascending
            if (string.IsNullOrEmpty(sortColumn))
            {
                sortColumn = "CustomerId";
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "asc";
            }
            if (sortColumn == "CustomerId")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(e => e.CustomerId);
                else
                    query = query.OrderBy(e => e.CustomerId);
            }
            if (sortColumn == "NationalId")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(e => e.Customers.NationalId);
                else
                    query = query.OrderBy(e => e.Customers.NationalId);
            }
            if (sortColumn == "Givenname")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(p => p.Customers.Givenname);
                else
                    query = query.OrderBy(p => p.Customers.Givenname);
            }
            if (sortColumn == "Givenname")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(p => p.Customers.Givenname);
                else
                    query = query.OrderBy(p => p.Customers.Givenname);
            }
            if (sortColumn == "Streetaddress")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(p => p.Customers.Streetaddress);
                else
                    query = query.OrderBy(p => p.Customers.Streetaddress);
            }
            if (sortColumn == "City")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(p => p.Customers.City);
                else
                    query = query.OrderBy(p => p.Customers.City);
            }

            return query.Where(r => r.Customers.IsActive == true && r.Type == "Owner").GetPaged(page, 50); //5 is the pagesize
        }

        public PagedResult<Customer> ListInactiveCustomers(int customerId, string sortColumn, string sortOrder, int page, string searchWord)
        {
            var query = _context.Customers.AsQueryable();
            if (!string.IsNullOrEmpty(searchWord))
            {
                query = query.Where(r => r.Givenname.Contains(searchWord) ||
                r.Streetaddress.Contains(searchWord) || r.City.Contains(searchWord));
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
            if (sortColumn == "streetaddress")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(p => p.Streetaddress);
                else
                    query = query.OrderBy(p => p.Streetaddress);
            }
            if (sortColumn == "City")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(p => p.City);
                else
                    query = query.OrderBy(p => p.City);
            }

            return query.Where(r => r.IsActive == false).GetPaged(page, 25); //5 is the pagesize
        }
    }
}