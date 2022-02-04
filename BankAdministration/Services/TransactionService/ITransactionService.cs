using BankAdministration.Models;

namespace BankAdministration.Services

{
    public interface ITransactionService
    {
        public Transaction CreateTransactionForTransfer(int accountId, int accountNo, decimal amount, decimal balance, string operation, string bank, string account, string symbol, AccountType type);
        public Transaction CreateTransactionForWithdraw(int accountId, decimal amount, decimal balance, AccountType type);
        public Transaction CreateTransactionForDeposit(int accountId, decimal amount, decimal balance, string comment, AccountType type);
        public void Update(Transaction transaction);
    }
}