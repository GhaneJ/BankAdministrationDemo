using BankAdministration.Models;

namespace BankAdministration.Services

{
    public interface ITransactionService
    {
        public Transaction CreateTransaction(int accountId, int toAccount, int accountNo, int amount, decimal balance, string type, string operation, string bank, Account account, string symbol, int id);
        public void Update(Transaction transaction);

    }
}