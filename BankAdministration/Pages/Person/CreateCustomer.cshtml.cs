using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAdministration.Pages
{
    [BindProperties]
    public class CreateCustomerModel : PageModel
    {
        private readonly BankContext _context;

        public string CGender { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(10)]
        public string PostalCode { get; set; }
        [StringLength(50)]
        [Required]
        public string City { get; set; }

        public DateTime BirthDate { get; set; }
        public string Phonenumber { get; set; }
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneCountryCode { get; set; }
        public string CNID { get; set; }

        public int CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; }



        public CreateCustomerModel(BankContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            FillCountryList();
        }

        private void FillCountryList()
        {
            Countries = _context.Countries.Select(e => new SelectListItem
            {
                Text = e.CountryName,
                Value = e.Id.ToString(),
            }).ToList();
            Countries.Insert(0, new SelectListItem
            {
                Text = "Var god välj...",
                Value = "0"
            });
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var person = new Customer
                {
                    Gender = CGender,
                    Givenname = FirstName,
                    Surname = LastName,
                    Streetaddress = Address,
                    Emailaddress = Email,
                    City = City,
                    Country = _context.Countries.First(r => r.Id == CountryId),
                    Zipcode = PostalCode,
                    Birthday = BirthDate,
                    Telephonenumber = Phonenumber,
                    Telephonecountrycode = PhoneCountryCode,
                    NationalId = CNID,
                    Active = true
                };
                _context.Customers.Add(person);
                _context.SaveChanges();
                return RedirectToPage("Customers");
            }
            return Page();
        }

    }
}
