using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BankAdministration.Services;
using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankAdministration.Pages.BankAccount
{
    public class DepositModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        [Range(10, 3000)]
        [BindProperty]
        public int Amount { get; set; }
        [BindProperty]
        public DateTime DepositDate { get; set; }

        [Required(ErrorMessage = "Skriv en kommentar, tack")]
        [MinLength(5)]
        [MaxLength(100)]
        [BindProperty]
        public string Comment { get; set; }
        [BindProperty]
        public AccountType AccType { get; set; }
        public List<SelectListItem> AccountTypes { get; set; }

        public DepositModel(IAccountService accountService, ITransactionService transactionService)
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
            DepositDate = DateTime.Now.AddDays(1).Date;
            Amount = 100;
            FillAccountTypesList();
        }


        public IActionResult OnPost(int accountId)
        {
            if (DepositDate < DateTime.Now.AddDays(1).Date)  //2022-01-11 00:00
            {
                ModelState.AddModelError("DepositDate", "Datum måste vara minst en dag fram ");
            }
            if (ModelState.IsValid)
            {
                var transaction = _transactionService.CreateTransactionForDeposit(accountId, Amount, Comment, AccType);
                _transactionService.Update(transaction);
                return RedirectToPage("Index");
            }
            FillAccountTypesList();
            return Page();
        }

    }
}
