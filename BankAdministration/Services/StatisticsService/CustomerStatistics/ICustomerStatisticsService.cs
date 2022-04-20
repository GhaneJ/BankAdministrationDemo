namespace BankAdministration.Services.StatisticsService.CustomerStatistics
{
    public interface ICustomerStatisticsService
    {
        public int NumberOfActiveCustomers();
        public int NumberOfMaleCustomers();
        public int NumberOfFemaleCustomers();
    }
}
