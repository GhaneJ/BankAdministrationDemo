using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BankAdministration.Pages.BankAccount
{
    public class TransferModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

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

        public TransferModel(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
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

        public IActionResult OnPost(int accountId, int toAccount)
        {
            FromAccount = accountId;
            ToAccount = toAccount;
            
            if (ModelState.IsValid)
            {
                if (FromAccount != ToAccount)
                {
                    var transaction1 = _transactionService.CreateTransactionForTransfer(FromAccount, FromAccount, Amount, Operation, Bank, Symbol, AccType);
                    _transactionService.Update(transaction1);
                    var transaction2 = _transactionService.CreateTransactionForTransfer(ToAccount, FromAccount, Amount, Operation, Bank, Symbol, AccType);
                    _transactionService.Update(transaction2);
                    return RedirectToPage("Transaction","OnGet", new {accountId = transaction1.AccountId });
                }
            }
            FillAccountTypesList();
            return Page();
        }
    }
}