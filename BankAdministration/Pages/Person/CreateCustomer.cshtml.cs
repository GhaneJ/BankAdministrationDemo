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
    [Authorize(Roles = "Admin")]
    
    public class CreateCustomerModel : PageModel
    {
        private readonly BankContext _context;
        //private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        //private readonly ICountryService _countryService;

        [Required(ErrorMessage = "Ange kön, tack")]
        [BindProperty]
        public string Gender { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Ange förnamn")]
        [BindProperty]
        public string Givenname { get; set; }

        [Required(ErrorMessage = "Ange eftenamn")]
        [BindProperty]
        [StringLength(100)]
        public string Surname { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Gatuadress måste anges")]
        [BindProperty]
        public string Streetaddress { get; set; }

        [Required(ErrorMessage = "Postnumret saknas")]
        [BindProperty]
        public string Zipcode { get; set; }

        [Required(ErrorMessage = "Rutan kan inte vara tom")]
        [BindProperty]
        [StringLength(50)]
        public string City { get; set; }
        
        [BindProperty]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ange födelsedatum")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Telefonnumret saknas")]
        [BindProperty]
        public string Telephonenumber { get; set; }

        [StringLength(150)]
        [EmailAddress]
        [Required(ErrorMessage = "Skriv e-post")]
        [BindProperty]
        public string Emailaddress { get; set; }

        [Required(ErrorMessage = "Riktnumret saknas")]
        [BindProperty]
        public string Telephonecountrycode { get; set; }

        [RegularExpression("^(\\d{10}|\\d{12}|\\d{6}-\\d{4}|\\d{8}-\\d{4}|\\d{8} \\d{4}|\\d{6} \\d{4})", ErrorMessage = "Personnumret är ogiltigt")]
        [Required(ErrorMessage = "Ange personnummer, tack")]
        [BindProperty]
        public string NationalId { get; set; }

        [BindProperty]
        public PersonGender PGender { get; set; }
        public List<SelectListItem> PersonGenders { get; set; }
        public int CustomerId { get; set; }
        public int CountryId { get; set; }
        public int AccountId { get; set; }
        public Country? Country { get; set; }
        public Country CountryCode { get; set; }
        public bool IsActive { get; set; } = true;
        public string Frequency { get; set; }
        public decimal Balance { get; set; }
        public DateTime Created { get; set; }

        public List<SelectListItem> Countries { get; set; }

        private void FillPersonGendersList()
        {
            PersonGenders = Enum.GetValues<PersonGender>()
                .Select(r => new SelectListItem
                {
                    Value = r.ToString(),
                    Text = r.ToString()
                }).ToList();
        }

        public CreateCustomerModel(BankContext context, IAccountService accountService, ICustomerService customerService, ICountryService countryService)
        {
            //_accountService = accountService;
            _customerService = customerService;
            //_countryService = countryService;
            _context = context;
        }

        public void OnGet()
        {
            //_countryService.FillCountryList();
            FillCountryList();
            FillPersonGendersList();
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

        public IActionResult OnPost(int countryId, int customerId)
        {
            Balance = 100;
            Created = DateTime.Now;
            Frequency = "Monthly";


            if (ModelState.IsValid)
            {
                _customerService.CreateCustomer(Gender, Givenname, Surname, Streetaddress, Emailaddress, City, Zipcode, Birthday, Telephonenumber, Telephonecountrycode, NationalId, countryId, IsActive);
                                
                return RedirectToPage("Customers");
            }
            FillCountryList();
            FillPersonGendersList();
            return Page();
        }

    }
}
