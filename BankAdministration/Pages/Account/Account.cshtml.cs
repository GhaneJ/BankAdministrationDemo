using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAdministration.Pages.Account
{
    public class AccountModel : PageModel
    {
        private readonly BankContext _context;

        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public Decimal Balance { get; set; }

        public AccountModel(BankContext context)
        {
            _context = context;
        }
        public void OnGet(int uniqueId)
        {
            var e = _context.Accounts.First(e => e.AccountId == uniqueId);
            AccountId = e.AccountId;
            Frequency = e.Frequency;
            Created = e.Created;
            Balance = e.Balance;
        }
    }
}
