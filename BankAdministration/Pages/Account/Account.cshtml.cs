using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankAdministration.Pages.Account
{
    public class AccountModel : PageModel
    {
        private readonly BankContext _context;

        public class Item
        {
            public int AccountId { get; set; }
            public int CustomerId { get; set; }
            public string Givenname { get; set; }
            public string Surname { get; set; }
            public string Frequency { get; set; }
            public DateTime CreateDate { get; set; }
            public decimal Balance { get; set; }
            public string AccountType { get; set; }
        }
        

        public List<Item> Items { get; set; }

        public AccountModel(BankContext context)
        {
            _context = context;
        }

        public void OnGet(int uniqueId)
        {
            //Items = _context.Accounts.Where(c => c.AccountId == uniqueId).Join(_context.Dispositions, ac => ac.AccountId, di => di.AccountId, (ac, di) => new { ac, di }).Select(e=> new Item
            //{
            //    AccountId = e.ac.AccountId,
            //    Balance = e.ac.Balance,
            //    Frequency = e.ac.Frequency,
            //    CreateDate = e.ac.Created,
            //    AccountType = e.di.Type
            //}).ToList();

            var c = _context.Customers.Include(e => e.Dispositions).ThenInclude(e => e.Account).First(e => e.CustomerId == uniqueId);
            Items = new List<Item>();

            foreach(var disp in c.Dispositions)
            {
                Items.Add(new Item {
                    AccountId = disp.AccountId,
                    AccountType = disp.Type,
                    Balance = disp.Account.Balance,
                    CreateDate = disp.Account.Created,
                    CustomerId = disp.CustomerId,
                    Frequency = disp.Account.Frequency
                    
                    
                });
            }

            //Items = _context.Customers.Where(di => di.CustomerId == uniqueId).Join(_context.Dispositions, cu => cu.CustomerId, di => di.AccountId, (cu, di) => new { cu, di })
            //.Join(_context.Accounts, ac => ac.di.CustomerId, acc => acc.AccountId, (ac, acc) => new {ac, acc}).Select(e => new Item
            //{
            //    CustomerId = e.ac.cu.CustomerId,
            //    Givenname = e.ac.cu.Givenname,
            //    Surname = e.ac.cu.Surname,
            //    Balance = e.ac.di.Account.Balance,
            //    AccountType = e.ac.di.Type
            //}).ToList();


            //var acco = _context.Accounts.Where(c => c.AccountId == uniqueId).Join(_context.Dispositions, ac => ac.AccountId, di => di.AccountId, (ac, di) => new { ac, di }).First(r => r.ac.AccountId == uniqueId);
            //AccountId = acco.ac.AccountId;
            //Frequency = acco.ac.Frequency;
            //Balance = acco.ac.Balance;
            //AccountType = acco.di.Type;


        }
    }
}
