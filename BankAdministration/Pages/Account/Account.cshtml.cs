using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAdministration.Pages.Account
{
    public class AccountModel : PageModel
    {
        private readonly BankContext _context;

        
            public int AccountId { get; set; }
            public string Frequency { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Balance { get; set; }
        
        //public List<Item> Items { get; set; }

        public AccountModel(BankContext context)
        {
            _context = context;
        }

        public void OnGet(int uniqueId)
        {
            //Accounts = _accountService.GetAccount(uniqueId).Select(r => new AccountViewModel
            //{
            //    Id = r.AccountId,
            //    AccountNo = r.AccountId.ToString(),
            //    Balance = r.Balance
            //}).ToList();


            //Items = _context.Accounts.Select(e=> new Item
            //{
            //    Id = uniqueId,
            //    AccountNo = e.AccountId.ToString(),
            //    Balance = e.Balance,
            //}).ToList();

            //return _context.Accounts.First(e => e.AccountId == uniqueId);

            var b = _context.Accounts.First(e => e.AccountId == uniqueId);
            AccountId = b.AccountId;
            Frequency = b.Frequency;
            CreateDate = b.Created;
            Balance = b.Balance;
        }
    }
}
