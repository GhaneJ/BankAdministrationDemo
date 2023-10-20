namespace BankAdministration.Services;

using BankAdministration.Models;

public interface IAccountService
{
    //public PagedResult<Disposition> ListAccounts(int accountId, string sortColumn, string sortOrder, int page, string searchWord);
    public List<Account> GetAll();

    void Update(Account account);
    Account GetAccount(int id);
    public Account CreateAccount(decimal balance, DateTime created, string frequency);
}