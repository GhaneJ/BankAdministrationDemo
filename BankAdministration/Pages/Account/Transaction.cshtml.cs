using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankAdministration.Pages.Account
{
    public class TransactionModel : PageModel
    {
        private readonly BankContext _context;

        public class Item
        {
            public int AccountId { get; set; }
            public int TransactionId { get; set; }
            public DateTime Date { get; set; }
            public string Type { get; set; }
            public string Operation { get; set; }
            public decimal Balance { get; set; }
            public string Symbol { get; set; }
            public string Bank { get; set; }
            public string Account { get; set; }
        }
        public List<Item> Items { get; set; }


        public TransactionModel(BankContext context)
        {
            _context = context;
        }
        public void OnGet(int accountId)
        {
            var e = _context.Accounts.Include(t => t.Transactions).First(e=>e.AccountId == accountId);
            Items = new List<Item>();

            foreach (var trans in e.Transactions)
            {
                Items.Add(new Item
                {
                    TransactionId = trans.TransactionId,
                    AccountId = trans.AccountId,
                    Date = trans.Date,
                    Balance = trans.Balance,
                    Type = trans.Type,
                    Operation = trans.Operation,
                    Symbol = trans.Symbol,
                    Bank = trans.Bank,
                    Account = trans.Account
                });
            }
        }
    }
}
