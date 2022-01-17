using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BankAdministration.Services;

namespace BankAdministration.Pages.Account
{
    [BindProperties]
    public class WithdrawModel : PageModel
    {
        private readonly IAccountService _accountService;
        //private readonly IRealAccountService _realAccountService;

        [Range(10, 3000)]
        public int Amount { get; set; }


        public WithdrawModel(IAccountService accountService, IRealAccountService realAccountService)
        {
            _accountService = accountService;
            //_realAccountService = realAccountService;
        }

        public void OnGet(int accountId)
        {
            Amount = 100;
        }


        public IActionResult OnPost(int accountId)
        {


            var account = _accountService.GetAccount(accountId);
            if (account.Balance < Amount)
            {
                ModelState.AddModelError("Amount", "F�r stort belopp");
            }



            if (ModelState.IsValid)
            {
                account.Balance -= Amount;
                _accountService.Update(account);
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}