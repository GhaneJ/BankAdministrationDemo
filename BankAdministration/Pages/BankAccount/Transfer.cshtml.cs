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
        public TransferModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public int ToAccount { get; set; }
        public int FromAccount { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }


        public void OnGet(int accountId)
        {
            
        }

        public IActionResult OnPost(int accountId, int toAccount, decimal amount)
        {
            FromAccount = accountId;
            ToAccount = toAccount;
            if (ModelState.IsValid)
            {
                var account1 = _accountService.GetAccount(accountId);
                var account2 = _accountService.GetAccount(toAccount);
                account1.Balance -= amount;
                account2.Balance += amount;
                _accountService.Update(account1);
                _accountService.Update(account2);
            }
            

            return Page();
        }


    }

    
}