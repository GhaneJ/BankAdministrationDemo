using BankAdministration.Data;
using BankAdministration.Infrastructure.Paging;
using BankAdministration.Models;

namespace BankAdministration.Services;

public interface IAccountService
{
    public PagedResult<Disposition> ListAccounts(int accountId, string sortColumn, string sortOrder, int page, string searchWord);
    public List<Account> GetAll();

    void Update(Account account);
    Account GetAccount(int id);

}