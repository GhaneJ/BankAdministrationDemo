using BankAdministration.Models;

namespace BankAdministration.Services.StatisticsService.CustomerStatistics
{
    public class CustomerStatisticsService : ICustomerStatisticsService
    {
        private readonly BankContext _context;

        public CustomerStatisticsService(BankContext context)
        {
            _context = context;
        }

        public int NumberOfActiveCustomers()
        {
            return _context.Customers.Where(r => r.IsActive == true).Count();
        }

        public int NumberOfMaleCustomers()
        {
            return _context.Customers.Where(c => c.Gender == "male").Count();
        }

        public int NumberOfFemaleCustomers()
        {
            return _context.Customers.Where(c => c.Gender == "female").Count();
        }
    }
}
