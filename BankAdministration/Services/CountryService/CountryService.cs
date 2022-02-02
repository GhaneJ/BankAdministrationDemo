using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankAdministration.Services
{
    public class CountryService : ICountryService
    {
        private readonly BankContext _context;

        public int CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; }

        public CountryService(BankContext context)
        {
            _context = context;
        }

        public Country GetCountry(int countryId)
        {
            return _context.Countries.First(r => r.Id == countryId);
        }
        public void FillCountryList()
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
    }
}
