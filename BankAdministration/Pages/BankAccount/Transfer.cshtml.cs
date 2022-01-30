using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankAdministration.Pages.BankAccount
{
    public class TransferModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly BankContext _context;
        public TransferModel(IAccountService accountService, ITransactionService transactionService, BankContext context)
        {
            _accountService = accountService;
            _transactionService = transactionService;
            _context = context;

        }
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



        public void OnGet(int accountId)
        {

        }

        public IActionResult OnPost(int accountId, int toAccount, int accountNo, int amount, decimal balance, string type, string operation, string bank, string account, string symbol, int id)
        {
            //FromAccount = accountId;
            //ToAccount = toAccount;
            //Amount = amount;
            //Balance = balance;
            //Type = type;
            //Operation = operation;
            //Bank = bank;
            //Symbol = symbol;
            //Account = account;
            //AccountNavigation = _context.Accounts.First(e=>e.AccountId == accountId);            
            if (ModelState.IsValid)
            {
                var account1 = _accountService.GetAccount(accountId);
                var account2 = _accountService.GetAccount(toAccount);
                var transaction1 = _transactionService.CreateTransaction(accountId, toAccount, accountId, amount, balance, type, operation, bank, account1, symbol, id);
                var transaction2 = _transactionService.CreateTransaction(toAccount, toAccount, toAccount, amount, balance, type, operation, bank, account2, symbol, id);
                account1.Balance -= amount;
                account2.Balance += amount;

                _accountService.Update(account1);
                _accountService.Update(account2);
                _transactionService.Update(transaction1);
                _transactionService.Update(transaction2);
                //_context.SaveChanges();
                return RedirectToPage("Index");
            }
            return Page();
        }


    }


}