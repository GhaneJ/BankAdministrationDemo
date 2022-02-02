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

        public IActionResult OnPost(int accountId, int toAccount, int amount, string type, string operation, string bank, string account, string symbol)
        {
            int fromAccount = accountId;
            FromAccount = fromAccount;
            ToAccount = toAccount;
            Amount = amount;
            //Balance = balance;
            Type = type;
            Operation = operation;
            Bank = bank;
            Symbol = symbol;
            Account = account;
            AccountNavigation = _context.Accounts.First(e => e.AccountId == accountId);
            if (ModelState.IsValid)
            {
                var account1 = _accountService.GetAccount(FromAccount);
                var account2 = _accountService.GetAccount(ToAccount);
                var transaction1 = _transactionService.CreateTransaction(accountId, fromAccount, toAccount, accountId, amount, account1.Balance, type, operation, bank, account1, symbol);
                var transaction2 = _transactionService.CreateTransaction(toAccount, fromAccount, toAccount, toAccount, amount, account2.Balance, type, operation, bank, account2, symbol);
                _transactionService.Update(transaction1);
                _transactionService.Update(transaction2);
                return RedirectToPage("Index");
            }
            return Page();
        }


    }


}