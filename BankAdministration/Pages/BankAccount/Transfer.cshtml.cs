using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BankAdministration.Pages.BankAccount
{
    [Authorize(Roles = "Admin")]
    public class TransferModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly BankContext _context;

        [BindProperty]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "Detta konto finns inte")] //Only positive account numbers
        public int ToAccount { get; set; }
        public int FromAccount { get; set; }
        [BindProperty]
        public int AccountId { get; set; }
        [BindProperty]
        [Range(10, 3000)]
        public decimal Amount { get; set; }
        [BindProperty]
        public string Operation { get; set; }
        [BindProperty]
        public string Bank { get; set; }
        [BindProperty]
        public string Symbol { get; set; }
        public Account AccountNavigation { get; set; }
        [BindProperty]        
        public AccountType AccType { get; set; }
        public List<SelectListItem> AccountTypes { get; set; }

        public TransferModel(IAccountService accountService, ITransactionService transactionService, BankContext context)
        {
            _accountService = accountService;
            _transactionService = transactionService;
            _context = context;
        }
        private void FillAccountTypesList()
        {
            AccountTypes = Enum.GetValues<AccountType>()
                .Select(r => new SelectListItem
                {
                    Value = r.ToString(),
                    Text = r.ToString()
                }).ToList();
        }

        public void OnGet(int accountId)
        {
            Operation = "Överföring";
            FillAccountTypesList();
        }

        public IActionResult OnPost(int accountId, int toAccount, decimal balance)
        {
            var account = _accountService.GetAccount(accountId);
            FromAccount = accountId;
            ToAccount = toAccount;

            
            if (account.Balance < Amount)
            {
                ModelState.AddModelError("Amount", "För stort belopp");
            }
            if (ModelState.IsValid)
            {
                if (_context.Accounts.Any(o => o.AccountId == ToAccount)) //kollar om kontot finns i Db:n eller ej
                {
                    //
                    if (FromAccount != ToAccount)
                    {
                        var transaction1 = _transactionService.CreateTransactionForTransfer(FromAccount, FromAccount, Amount, Operation, Bank, Symbol, AccType);
                        _transactionService.Update(transaction1);
                        var transaction2 = _transactionService.CreateTransactionForTransfer(ToAccount, FromAccount, Amount, Operation, Bank, Symbol, AccType);
                        _transactionService.Update(transaction2);
                        return RedirectToPage("Transaction", "OnGet", new { accountId = transaction1.AccountId });
                    }
                    else if (ToAccount == FromAccount)
                    {
                        ModelState.AddModelError("ToAccount", "det går inte att överföra till sitt eget konto!");
                    }
                }
                else
                {
                    ModelState.AddModelError("ToAccount", "Detta konto finns inte");
                }
                
            }
            FillAccountTypesList();
            return Page();
        }
    }
}