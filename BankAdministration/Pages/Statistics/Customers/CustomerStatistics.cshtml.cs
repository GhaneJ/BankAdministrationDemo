using BankAdministration.Services;
using BankAdministration.Services.CustomerService.GeoLocation;
using BankAdministration.Services.StatisticsService.CustomerStatistics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAdministration.Pages
{
    public class CustomerStatisticsModel : PageModel
    {
        private readonly ICustomerStatisticsService _customerStatisticsService;
        private readonly ILocationService _locationService;
        
        public int ActiveCustomers { get; set; }

        // Customers' gender
        public int MaleCustomers { get; set; }
        public int FemaleCustomers { get; set; }

        //Customers' geolocation
        public int SwedishCustomers { get; set; }
        public int NorwegianCustomers { get; set; }
        public int DanishCustomers { get; set; }
        public int FinnishCustomers { get; set; }

        public CustomerStatisticsModel(ICustomerStatisticsService customerStatisticsService, ILocationService locationService)
        {
            _customerStatisticsService = customerStatisticsService;
            _locationService = locationService;
        }

        public void OnGet()
        {
            ActiveCustomers = _customerStatisticsService.NumberOfActiveCustomers();            
            MaleCustomers = _customerStatisticsService.NumberOfMaleCustomers();
            FemaleCustomers = _customerStatisticsService.NumberOfFemaleCustomers();

            SwedishCustomers = _locationService.NumberOfSwedishCustomers();
            NorwegianCustomers = _locationService.NumberOfNorwegianCustomers();
            DanishCustomers = (_locationService.NumberOfDanishCustomers());
            FinnishCustomers = _locationService.NumberOfFinnishCustomers();
        }

    }
}
