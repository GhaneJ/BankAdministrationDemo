using BankAdministration.Infrastructure.Paging;
using BankAdministration.Models;

namespace BankAdministration.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankContext _context;
        public CustomerService(BankContext context)
        {
            _context = context;
        }
        public PagedResult<Customer> ListCustomers(int customerId, string sortColumn, string sortOrder, int page, string searchWord)
        {
            //Asquerable() converts the variable to a queryable IEnumerable(Without it, no data was being loaded)
            //var query = _context.Customers.AsQueryable().Where(r => r.CustomerId == customerId);
            var query = _context.Customers.AsQueryable();
            if (!string.IsNullOrEmpty(searchWord))
            {
                query = query.Where(r=>r.Givenname.Contains(searchWord));
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
                    query = query.OrderByDescending(p=>p.Givenname);
                else
                    query = query.OrderBy(p=>p.Givenname);
            }
            if (sortColumn == "streetaddress")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(p=>p.Streetaddress);
                else
                    query = query.OrderBy(p=>p.Streetaddress);
            }
            if (sortColumn == "City")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(p=>p.City);
                else
                    query = query.OrderBy(p=>p.City);
            }

            return query.GetPaged(page, 25); //5 is the pagesize
        }
    }
}