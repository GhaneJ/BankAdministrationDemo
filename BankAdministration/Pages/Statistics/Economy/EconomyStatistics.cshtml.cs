namespace BankAdministration.Pages.Statistics;

using BankAdministration.Services.StatisticsService.EconomyStatistics;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class EconomyStatisticsModel : PageModel
{
    private readonly IEconomyStatisticsService _economyStatisticsService;

    public int AvailableAccounts { get; set; }
    public string SumOfBalances { get; set; }
    public int IssuedCards { get; set; }
    public int NumberOfLoans { get; set; }

    public EconomyStatisticsModel(IEconomyStatisticsService economyStatisticsService)
    {
        _economyStatisticsService = economyStatisticsService;
    }
    public void OnGet()
    {
        AvailableAccounts = _economyStatisticsService.NumberOfAvailabeAccounts();
        SumOfBalances = _economyStatisticsService.SumOfAccountBalances();
        IssuedCards = _economyStatisticsService.NumberOfIssuedCards();
        NumberOfLoans = _economyStatisticsService.NumberOfLoans();
    }
}
