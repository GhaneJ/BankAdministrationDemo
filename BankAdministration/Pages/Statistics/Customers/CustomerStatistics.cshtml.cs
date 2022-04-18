using BankAdministration.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAdministration.Pages
{
    public class CustomerStatisticsModel : PageModel
    {
        private readonly IStatisticsService _statisticsService;

        
        public int ActiveCustomers { get; set; }        
        public int MaleCustomers { get; set; }
        public int FemaleCustomers { get; set; }


        public CustomerStatisticsModel(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public void OnGet()
        {
            ActiveCustomers = _statisticsService.NumberOfActiveCustomers();
            
            MaleCustomers = _statisticsService.NumberOfMaleCustomers();
            FemaleCustomers = _statisticsService.NumberOfFemaleCustomers();
        }

    }
}
