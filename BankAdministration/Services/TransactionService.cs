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

        public Transaction CreateTransaction(int accountId, int toAccount, int accountNo, int amount, decimal balance, string type, string operation, string bank, Account account, string symbol, int id)
        {
            FromAccount = accountId;
            ToAccount = toAccount;
            Amount = amount;
            Balance = balance;
            Type = type;
            Operation = operation;
            Bank = bank;
            Symbol = symbol;
            //Account = account;
            AccountNavigation = _context.Accounts.First(e => e.AccountId == accountId);
            //var account1 = _accountService.GetAccount(accountId);
            //var account2 = _accountService.GetAccount(toAccount);
            account = _accountService.GetAccount(accountNo);
            Transaction transaction = new Transaction();
            {
                AccountId = account.AccountId;
                transaction.Amount = Amount;
                transaction.Symbol = Symbol;
                transaction.Date = DateTime.Now;
                transaction.Type = Type;
                transaction.Operation = Operation;
                transaction.Bank = Bank;
                transaction.Balance = Balance;
                transaction.Account = Account;
                transaction.AccountNavigation = AccountNavigation;
            };
            _context.Transactions.Add(transaction);
            return transaction;
            //_context.SaveChanges();
        }
    }
}
