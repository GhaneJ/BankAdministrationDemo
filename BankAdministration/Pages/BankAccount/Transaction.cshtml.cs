using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankAdministration.Infrastructure.Paging;

namespace BankAdministration.Pages.BankAccount
{
    public class TransactionModel : PageModel
    {
        public string Name { get; set; }
        public int Id { get; set; }

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
            public decimal Amount { get; set; }
        }
        public List<Item> Items { get; set; }

        public int AccountId { get; set; }
        public decimal FinalBalance { get; set; }

        public TransactionModel(BankContext context)
        {
            _context = context;
        }

        public IActionResult OnGetFetchValue(int id)
        {
            return new JsonResult(new { value = id * 1000 });
        }

        public IActionResult OnGetFetchMore(int accountId, int pageNo)
        {

            var list = _context.Accounts
                .Where(e => e.AccountId == accountId)
                .SelectMany(e => e.Transactions)
                .OrderByDescending(e => e.TransactionId)
                .GetPaged(pageNo, 5).Results
                .Select(e => new Item
                {
                    TransactionId = e.TransactionId,
                    AccountId = e.AccountId,
                    Balance = e.Balance,
                    Symbol = e.Symbol,
                    Date = e.Date,
                    Amount = e.Amount,
                    Type = e.Type,
                    Account = e.Account,
                    Operation = e.Operation,
                    Bank = e.Bank
                }).ToList();

            return new JsonResult(new { items = list });
        }

        public void OnGet(int accountId)
        {
            AccountId = accountId;
            var e = _context.Accounts.Include(t => t.Transactions).First(e=>e.AccountId == accountId);
            Items = new List<Item>();

            FinalBalance = e.Balance;
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
                    Account = trans.Account,
                    Amount = trans.Amount
                });
                Items = Items.ToList();
            }
        }
    }
}
