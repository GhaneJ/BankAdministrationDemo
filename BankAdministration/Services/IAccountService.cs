using BankAdministration.Data;
using BankAdministration.Models;

namespace BankAdministration.Services;

public interface IAccountService
{
    public List<Account> GetAll();

    void Update(Account account);
    Account GetAccount(int id);

}