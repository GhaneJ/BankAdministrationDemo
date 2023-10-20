namespace BankAdministration.Pages.BankAccount;

using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
        public decimal Amount { get; set; }
        public string Type { get; set; }

    }

    public List<Item> Items { get; set; }
    public int CurrentPage { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
    public string SearchWord { get; set; }
    public int PageCount { get; set; }
    public int CustomerId { get; set; }
    public AccountModel(BankContext context)
    {
        _context = context;
    }

    public void OnGet(int customerId)
    {
        var c = _context.Dispositions.Include(e => e.Customers).Include(e => e.Account).Where(e => e.CustomerId == customerId);
        Items = new List<Item>();

        foreach (var disp in c)
        {
            Items.Add(new Item
            {
                AccountId = disp.AccountId,
                Type = disp.Type,
                Balance = disp.Account.Balance,
                CreateDate = disp.Account.Created,
                CustomerId = disp.CustomerId,
                Frequency = disp.Account.Frequency
            });
        }
    }
}
