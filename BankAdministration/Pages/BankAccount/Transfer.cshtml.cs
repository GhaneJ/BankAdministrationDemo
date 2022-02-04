using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BankAdministration.Pages.BankAccount
{
    public class TransferModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly BankContext _context;

        [BindProperty]
        public int ToAccount { get; set; }
        public int FromAccount { get; set; }
        [BindProperty]
        public int AccountId { get; set; }
        [BindProperty]
        public decimal Amount { get; set; }
        public int TransactionId { get; set; }
        [BindProperty]
        public Decimal Balance { get; set; }
        public DateTime Date { get; set; }
        
        [BindProperty]
        public string Operation { get; set; }
        [BindProperty]
        public string Bank { get; set; }
        [BindProperty]
        public string Symbol { get; set; }
        
        public string Account { get; set; }
        public Account AccountNavigation { get; set; }
        [BindProperty]
        public AccountType Type { get; set; }
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
            Amount = 100M;
            FillAccountTypesList();
        }

        public IActionResult OnPost(int accountId, int toAccount)
        {
            FromAccount = accountId;
            ToAccount = toAccount;
            if (ModelState.IsValid)
            {
                var transaction1 = _transactionService.CreateTransactionForTransfer(FromAccount, FromAccount, Amount, Balance, Operation, Bank, Account, Symbol, Type);
                _transactionService.Update(transaction1);
                var transaction2 = _transactionService.CreateTransactionForTransfer(ToAccount, FromAccount, Amount, Balance, Operation, Bank, Account, Symbol, Type);
                _transactionService.Update(transaction2);
                return RedirectToPage("Index");
            }
            FillAccountTypesList();
            return Page();
        }


    }


}