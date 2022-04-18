using BankAdministration.Infrastructure.Paging;
using BankAdministration.Models;

namespace BankAdministration.Services
{
    public interface IStatisticsService
    {
        //Customer Statistics
        public int NumberOfActiveCustomers();
        public int NumberOfMaleCustomers();
        public int NumberOfFemaleCustomers();

        //Economy Statistics
        public int NumberOfAvailabeAccounts();
        public string SumOfAccountBalances();
        public int NumberOfIssuedCards();
        public int NumberOfLoans();

        //Pagination
        PagedResult<Customer> ListCustomers(int customerId, string sortColumn, string sortOrder, int page, string searchWord);
    }
}
