using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankAdministration.Pages
{
    public class CreateCustomerModel : PageModel
    {
        private readonly BankContext _context;
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Gender { get; set; }
        [BindProperty]
        public string Givenname { get; set; }
        [BindProperty]
        public string Surname { get; set; }
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
        [BindProperty]
        public string NationalId { get; set; }
        [BindProperty]
        public string Telephonecountrycode { get; set; }
        [BindProperty]
        public string Telephonenumber { get; set; }
        [BindProperty]
        public string Emailaddress { get; set; }

        public List<SelectListItem> AllCustomers { get; set; }

        public CreateCustomerModel(BankContext context)
        {
            _context = context;
        }

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var c = new Customer();
                _context.Customers.Add(c);
                c.Gender = Gender;
                c.Givenname = Givenname;
                c.Surname = Surname;
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

                _context.SaveChanges();
                return RedirectToPage("Index");
            }
            AllCustomers = _context.Customers.Select(e => new SelectListItem
            {
                Text = e.Givenname,
                Value = e.CustomerId.ToString()
            }).ToList();
            return Page();
        }

        public void OnGet()
        {
            AllCustomers = _context.Customers.Select(e => new SelectListItem
            {
                Text = e.Givenname,
                Value = e.CustomerId.ToString(),
            }).ToList();
        }
    }
}
