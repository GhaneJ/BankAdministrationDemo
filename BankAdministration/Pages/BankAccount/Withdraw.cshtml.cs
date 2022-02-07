using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BankAdministration.Services;
using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace BankAdministration.Pages.BankAccount
{
    [Authorize(Roles = "Admin")]
    public class WithdrawModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        //private readonly IRealAccountService _realAccountService;

        [Range(10, 3000)]
        [BindProperty]
        public int Amount { get; set; }
        public Decimal Balance { get; set; }
        [BindProperty]
        public DateTime Date { get; set; }
        [BindProperty]
        public AccountType Type { get; set; }
        public Account AccountNavigation { get; set; }
        public List<SelectListItem> AccountTypes { get; set; }

        public WithdrawModel(IAccountService accountService, ITransactionService transactionService, IRealAccountService realAccountService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
            //_realAccountService = realAccountService;
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
            Amount = 100;
            FillAccountTypesList();
        }


        public IActionResult OnPost(int accountId)
        {
            var account = _accountService.GetAccount(accountId);
            
            if (account.Balance < Amount)
            {
                ModelState.AddModelError("Amount", "För stort belopp");
            }

            if (ModelState.IsValid)
            {
                var transaction = _transactionService.CreateTransactionForWithdraw(accountId, Amount, Balance, Type);
                _transactionService.Update(transaction);
                return RedirectToPage("Index");
            }
            FillAccountTypesList();
            return Page();
        }
    }
}