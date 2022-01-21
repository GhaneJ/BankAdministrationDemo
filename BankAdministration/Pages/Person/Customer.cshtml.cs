using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankAdministration.Pages.Person
{
    public class CustomerModel : PageModel
    {
        private readonly BankContext _context;

        public class Item
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public string Address { get; set; }
            public string ZipCode { get; set; }
            public string City { get; set; }
            //public Country Country { get; set; }
            public string AreaCode { get; set; }
            public string PhoneNumber { get; set; }
            public DateTime Birthdate { get; set; }
            public string NID { get; set; }
            public string Email { get; set; }
            public bool Status { get; set; }


            public int AccountId { get; set; }
            public string Frequency { get; set; }
            public DateTime Created { get; set; }
            public decimal Balance { get; set; }
        }
        public List<Item> Items { get; set; }


        public CustomerModel(BankContext context)
        {
            _context = context;
        }
        public void OnGet(int accountId)
        {
            var c = _context.Accounts.Include(e => e.Dispositions).ThenInclude(e => e.Customers).First(e => e.AccountId == accountId);
            Items = new List<Item>();
            foreach (var disp in c.Dispositions)
            {
                Items.Add(new Item
                {
                    FirstName = disp.Customers.Givenname,
                    LastName = disp.Customers.Surname,
                    Gender = disp.Customers.Gender,
                    Address = disp.Customers.Streetaddress,
                    ZipCode = disp.Customers.Zipcode,
                    City = disp.Customers.City,
                    AreaCode = disp.Customers.Telephonecountrycode,
                    PhoneNumber = disp.Customers.Telephonenumber,
                    NID = disp.Customers.NationalId,
                    Birthdate = (DateTime)disp.Customers.Birthday,
                    Email = disp.Customers.Emailaddress,
                    Status = (bool)disp.Customers.Active
                });
            }
        }
    }
}
