namespace BankAdministration.Services.StatisticsService.EconomyStatistics
{
    public interface IEconomyStatisticsService
    {
        public int NumberOfAvailabeAccounts();
        public string SumOfAccountBalances();
        public int NumberOfIssuedCards();
        public int NumberOfLoans();
    }
}
