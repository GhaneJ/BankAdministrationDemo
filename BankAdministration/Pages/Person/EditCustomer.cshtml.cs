namespace BankAdministration.Pages;

using BankAdministration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]

public class EditCustomerModel : PageModel
{
    private readonly BankContext _context;
    [BindProperty]
    public int CustomerId { get; set; }
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
    public DateTime Birthday { get; set; }
    [BindProperty]
    public string NationalId { get; set; }
    [BindProperty]
    public string Telephonecountrycode { get; set; }
    [BindProperty]
    public string Telephonenumber { get; set; }
    [BindProperty]
    public string Emailaddress { get; set; }
    [BindProperty]
    public bool Active { get; set; }
    [BindProperty]
    public int CountryId { get; set; }
    [BindProperty]
    public PersonGender PGender { get; set; }
    public List<SelectListItem> PersonGenders { get; set; }
    public List<SelectListItem> Countries { get; set; }
    public List<SelectListItem> AllCustomers { get; set; }

    public EditCustomerModel(BankContext context)
    {
        _context = context;
    }

    private void FillPersonGendersList()
    {
        PersonGenders = Enum.GetValues<PersonGender>()
            .Select(r => new SelectListItem
            {
                Value = r.ToString(),
                Text = r.ToString()
            }).ToList();
    }

    public IActionResult OnPost(int id)
    {
        if (ModelState.IsValid)
        {
            var person = _context.Customers.First(c => c.CustomerId == id);
            person.Givenname = Givenname;
            person.Surname = Surname;
            person.Gender = Gender;
            person.Streetaddress = Streetaddress;
            person.Zipcode = Zipcode;
            person.City = City;
            person.Country = _context.Countries.First(e => e.Id == CountryId);
            person.Birthday = Birthday;
            person.NationalId = NationalId;
            person.Telephonecountrycode = Telephonecountrycode;
            person.Telephonenumber = Telephonenumber;
            person.Emailaddress = Emailaddress;
            person.IsActive = Active;

            _context.SaveChanges();
            return RedirectToPage("Customers");
        }
        AllCustomers = _context.Customers.Select(e => new SelectListItem
        {
            Text = e.Givenname,
            Value = e.CustomerId.ToString()
        }).ToList();
        FillCountryList();
        FillPersonGendersList();
        return Page();
    }

    public void OnGet(int id)
    {
        var person = _context.Customers.Where(r => r.IsActive == true || r.IsActive == false).Include(e => e.Country).First(c => c.CustomerId == id);
        Givenname = person.Givenname;
        Surname = person.Surname;
        Gender = person.Gender;
        Streetaddress = person.Streetaddress;
        Zipcode = person.Zipcode;
        City = person.City;
        Birthday = person.Birthday.Value;
        NationalId = person.NationalId;
        Telephonecountrycode = person.Telephonecountrycode;
        Telephonenumber = person.Telephonenumber;
        Emailaddress = person.Emailaddress;
        Active = (bool)person.IsActive;
        CountryId = person.Country.Id;
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
    }
}
