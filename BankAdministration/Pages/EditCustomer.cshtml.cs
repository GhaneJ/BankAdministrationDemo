using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankAdministration.Pages
{
    public class EditCustomerModel : PageModel
    {
        private readonly BankContext _context;
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Givenname { get; set; }
        [BindProperty]
        public string Surname { get; set; }
        [BindProperty]
        public string Gender { get; set; }
        [BindProperty]
        public string Streetaddress { get; set; }
        [BindProperty]
        public string Zipcode { get; set; }
        [BindProperty]
        public string City { get; set; }
        [BindProperty]
        public string Country { get; set; }
        [BindProperty]
        public string CountryCode { get; set; }
        [BindProperty]
        public DateTime Birthday { get; set; }
        
        public string NationalId { get; set; }
        [BindProperty]
        public string Telephonecountrycode { get; set; }
        [BindProperty]
        public string Telephonenumber { get; set; }
        [BindProperty]
        public string Emailaddress { get; set; }
        public bool Active { get; set; }

        public List<SelectListItem> AllCustomers { get; set; }

        public EditCustomerModel(BankContext context)
        {
            _context = context;
        }

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var c = _context.Customers.First(c => c.CustomerId == id);
                c.Givenname = Givenname;
                c.Surname = Surname;
                c.Gender = Gender;
                c.Streetaddress = Streetaddress;
                c.Zipcode = Zipcode;
                c.City = City;
                c.Country = Country;
                c.CountryCode = CountryCode;
                c.Birthday = Birthday;
                c.NationalId = NationalId;
                c.Telephonecountrycode = Telephonecountrycode;
                c.Telephonenumber = Telephonenumber;
                c.Emailaddress = Emailaddress;
                c.Active = Active;

                _context.SaveChanges();
                return RedirectToPage("Customers");
            }
            AllCustomers = _context.Customers.Select(e => new SelectListItem
            {
                Text = e.Givenname,
                Value = e.CustomerId.ToString()
            }).ToList();
            return Page();
        }

        public void OnGet(int id)
        {
            var c = _context.Customers.First(c => c.CustomerId == id);
            Givenname = c.Givenname;
            Surname = c.Surname;
            Gender = c.Gender;
            Streetaddress = c.Streetaddress;
            Zipcode = c.Zipcode;
            City = c.City;
            Country = c.Country;
            CountryCode = c.CountryCode;
            Birthday = c.Birthday.Value;
            NationalId = c.NationalId;
            Telephonecountrycode = c.Telephonecountrycode;
            Telephonenumber = c.Telephonenumber;
            Emailaddress = c.Emailaddress;
            Active = c.Active.HasValue;
        }
    }
}
