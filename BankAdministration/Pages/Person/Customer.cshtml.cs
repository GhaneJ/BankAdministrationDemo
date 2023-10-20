namespace BankAdministration.Pages.Person;

using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class CustomerModel : PageModel
{
    private readonly BankContext _context;

    public class Item
    {
        //Personuppgifter
        public int CustomerId { get; set; }
        public string Gender { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string NationalId { get; set; }
        public DateTime Birthday { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }
        public bool IsActive { get; set; }

        public Country Country { get; set; }
        //Kontouppgifter
        public string Type { get; set; }
        public int AccountId { get; set; }

        public decimal Balance { get; set; }
        public DateTime Created { get; set; }
        public string Frequency { get; set; }
    }
    public List<Item> Items { get; set; }
    public int AccountId { get; set; }
    public int CustomerId { get; set; }

    public CustomerModel(BankContext context)
    {
        _context = context;
    }
    public void OnGet(int customerId)
    {
        CustomerId = customerId;
        var c = _context.Customers.Include(e => e.Dispositions).ThenInclude(e => e.Account).First(e => e.CustomerId == customerId);
        Items = new List<Item>();
        foreach (var disp in c.Dispositions)
        {
            Items.Add(new Item
            {
                AccountId = disp.AccountId,
                Givenname = disp.Customers.Givenname,
                Surname = disp.Customers.Surname,
                Gender = disp.Customers.Gender,
                Streetaddress = disp.Customers.Streetaddress,
                Zipcode = disp.Customers.Zipcode,
                City = disp.Customers.City,
                Telephonecountrycode = disp.Customers.Telephonecountrycode,
                Telephonenumber = disp.Customers.Telephonenumber,
                NationalId = disp.Customers.NationalId,
                Birthday = (DateTime)disp.Customers.Birthday,
                Emailaddress = disp.Customers.Emailaddress,
                IsActive = (bool)disp.Customers.IsActive,
                Balance = disp.Account.Balance,
                Created = (DateTime)disp.Account.Created,
                Type = disp.Type
            });
        }
    }
}
