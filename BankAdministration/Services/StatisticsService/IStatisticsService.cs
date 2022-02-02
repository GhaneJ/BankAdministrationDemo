using BankAdministration.Infrastructure.Paging;
using BankAdministration.Models;

namespace BankAdministration.Services
{
    public interface IStatisticsService
    {
        public string activeCustomers();
        public string availabAccounts();
        public string sumOfAccountBalances();
        PagedResult<Customer> ListCustomers(int customerId, string sortColumn, string sortOrder, int page, string searchWord);
    }
}
