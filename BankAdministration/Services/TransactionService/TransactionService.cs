using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAdministration.Services
{
    [BindProperties]
    public class TransactionService : ITransactionService
    {
        private readonly BankContext _context;
        private readonly IAccountService _accountService;

        public int ToAccount { get; set; }
        public int FromAccount { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public int TransactionId { get; set; }
        public Decimal Balance { get; set; }
        public DateTime Date { get; set; }

        public string Type { get; set; }
        public string Operation { get; set; }
        public string Bank { get; set; }
        public string Symbol { get; set; }
        public string Account { get; set; }
        public Account AccountNavigation { get; set; }



        public TransactionService(BankContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }
        public List<Transaction> GetAll()
        {
            return _context.Transactions.ToList();
        }


        public void Update(Transaction transaction)
        {
            _context.SaveChanges();
        }

        public Transaction CreateTransaction(int accountId, int fromAccount, int toAccount, int accountNo, int amount, decimal balance, string type, string operation, string bank, Account account, string symbol)
        {
            AccountNavigation = _context.Accounts.First(e => e.AccountId == accountNo);
            Transaction transaction = new Transaction();
            {
                if (accountNo == fromAccount)
                {
                    transaction.Amount = -amount;
                }
                else
                {
                    transaction.Amount = amount;
                }
                transaction.Symbol = symbol;
                transaction.Date = DateTime.Now;
                transaction.Type = type;
                transaction.Operation = operation;
                transaction.Bank = bank;
                transaction.Balance = account.Balance + transaction.Amount;
                
                account.Balance = transaction.Balance;
                _accountService.Update(account);
                transaction.AccountNavigation = AccountNavigation;
            };
            _context.Transactions.Add(transaction);
            return transaction;
        }
    }
}
