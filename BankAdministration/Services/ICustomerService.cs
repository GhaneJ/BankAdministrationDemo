using BankAdministration.Infrastructure.Paging;
using BankAdministration.Models;

namespace BankAdministration.Services
{
    public interface ICustomerService
    {
        PagedResult<Customer> ListCustomers(int customerId, string sortColumn, string sortOrder, int page, string searchWord);
    }
}
