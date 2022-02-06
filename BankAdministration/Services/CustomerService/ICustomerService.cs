using BankAdministration.Infrastructure.Paging;
using BankAdministration.Models;

namespace BankAdministration.Services
{
    public interface ICustomerService
    {
        PagedResult<Customer> ListCustomers(int customerId, string sortColumn, string sortOrder, int page, string searchWord);
        PagedResult<Customer> ListInactiveCustomers(int customerId, string sortColumn, string sortOrder, int currentPage, string searchWord);
        public Customer GetCustomer(int customerId);
        public Customer CreateCustomer(string gender, string givenname, string surname, string streetaddress, string emailaddress, string city, string zipcode, DateTime birthday, string telephonenumber, string telephonecountrycode, string nationalId, int countryId, bool isactive);
        public void Update(Customer customer);
    }
}
