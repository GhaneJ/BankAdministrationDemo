using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAdministration.Pages
{
    //[Authorize(Roles = "Admin")]
    //[BindProperties]
    public class CreateCustomerModel : PageModel
    {
        private readonly BankContext _context;
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly ICountryService _countryService;

        public string Gender { get; set; }
        [StringLength(100)]
        public string Givenname { get; set; }
        [StringLength(100)]
        public string Surname { get; set; }
        [StringLength(100)]
        public string Streetaddress { get; set; }
        [StringLength(10)]
        public string Zipcode { get; set; }
        [StringLength(50)]
        [Required]
        public string City { get; set; }

        public DateTime BirthDay { get; set; }
        public string Telephonenumber { get; set; }
        [StringLength(150)]
        [EmailAddress]
        public string Emailaddress { get; set; }
        public string Telephonecountrycode { get; set; }
        public string NationalId { get; set; }

        public int CountryId { get; set; }
        public int AccountId { get; set; }
        public Country? Country { get; set; }

        public List<SelectListItem> Countries { get; set; }



        public CreateCustomerModel(BankContext context, IAccountService accountService, ICustomerService customerService, ICountryService countryService)
        {
            _accountService = accountService;
            _customerService = customerService;
            _countryService = countryService;
            _context = context;
        }

        public void OnGet()
        {
            //_countryService.FillCountryList();
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

        public IActionResult OnPost(string gender, string givenname, string surname, string streetaddress, string emailaddress, string city, string zipcode, string? countrycode, DateTime birthday, string telephonenumber, string telephonecountrycode, string nationalId, int countryId, bool isactive)
        {
            if (ModelState.IsValid)
            {
                _customerService.CreateCustomer(gender, givenname, surname, streetaddress, emailaddress, city, zipcode, countrycode, birthday, telephonenumber, telephonecountrycode, nationalId, countryId, isactive);

                var person = new Customer
                {
                    Gender = gender,
                    Givenname = givenname,
                    Surname = surname,
                    Streetaddress = streetaddress,
                    Emailaddress = emailaddress,
                    City = city,
                    Country = _countryService.GetCountry(countryId),
                    //Country = _context.Countries.First(r => r.Id == countryId),
                    Zipcode = zipcode,
                    Birthday = birthday,
                    Telephonenumber = telephonenumber,
                    Telephonecountrycode = telephonecountrycode,
                    NationalId = nationalId,
                    IsActive = true
                };
                _customerService.Update(person);

                return RedirectToPage("Customers");
            }
            return Page();
        }

    }
}
