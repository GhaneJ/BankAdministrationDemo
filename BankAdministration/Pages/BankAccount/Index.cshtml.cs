using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BankAdministration.Services;

namespace BankAdministration.Pages.BankAccount
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;
        public List<AccountViewModel> Account { get; set; }

        public class AccountViewModel
        {
            public int Id { get; set; }
            public string AccountNo { get; set; }
            public decimal Balance { get; set; }
        }

        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public void OnGet()
        {
            Account = _accountService.GetAll().Select(r => new AccountViewModel
            {
                Id = r.AccountId,
                AccountNo = r.AccountId.ToString(),
                Balance = r.Balance
            }).ToList();
        }
    }
}