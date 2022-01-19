using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankAdministration.Pages.Person
{
    public class CustomerModel : PageModel
    {
        private readonly BankContext _context;

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


        public CustomerModel(BankContext context)
        {
            _context = context;
        }
        public void OnGet(int uniqueId)
        {
            var e = _context.Customers.Include(e => e.Dispositions).ThenInclude(r => r.Account).First(e => e.CustomerId == uniqueId);
            FirstName = e.Givenname;
            LastName = e.Surname;
            Gender = e.Gender;
            Address = e.Streetaddress;
            ZipCode = e.Zipcode;
            City = e.City;
            AreaCode = e.Telephonecountrycode;
            PhoneNumber = e.Telephonenumber;
            NID = e.NationalId;
            Birthdate = (DateTime)e.Birthday;
            Email = e.Emailaddress;
            Status = (bool)e.Active;
        }

    }
}
