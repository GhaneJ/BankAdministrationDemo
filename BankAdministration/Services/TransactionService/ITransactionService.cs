namespace BankAdministration.Services;

using BankAdministration.Models;

public interface ITransactionService
{
    public Transaction CreateTransactionForTransfer(int accountId, int accountNo, decimal amount, string operation, string bank, string symbol, AccountType type);
    public Transaction CreateTransactionForWithdraw(int accountId, decimal amount, decimal balance, AccountType type);
    public Transaction CreateTransactionForDeposit(int accountId, decimal amount, string comment, AccountType type);
    public void Update(Transaction transaction);
}