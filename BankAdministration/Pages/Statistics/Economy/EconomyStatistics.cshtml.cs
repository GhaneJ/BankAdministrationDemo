using BankAdministration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAdministration.Pages.Statistics
{
    public class EconomyStatisticsModel : PageModel
    {
        private readonly IStatisticsService _statisticsService;

        public int AvailableAccounts { get; set; }
        public string SumOfBalances { get; set; }
        public int IssuedCards { get; set; }
        public int NumberOfLoans { get; set; }

        public EconomyStatisticsModel(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        public void OnGet()
        {
            AvailableAccounts = _statisticsService.NumberOfAvailabeAccounts();
            SumOfBalances = _statisticsService.SumOfAccountBalances();
            IssuedCards = _statisticsService.NumberOfIssuedCards();
            NumberOfLoans = _statisticsService.NumberOfLoans();
        }
    }
}
