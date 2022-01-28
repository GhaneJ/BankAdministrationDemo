using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BankAdministration.Services;

namespace BankAdministration.Pages.BankAccount
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;
        public List<KontoViewModel> Konton { get; set; }

        public class KontoViewModel
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
            Konton = _accountService.GetAll().Select(r => new KontoViewModel
            {
                Id = r.AccountId,
                AccountNo = r.AccountId.ToString(),
                Balance = r.Balance
            }).ToList();
        }
    }
}