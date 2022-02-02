using BankAdministration.Models;

namespace BankAdministration.Services
{
    public interface ICountryService
    {
        public Country GetCountry(int countryId);
        public void FillCountryList();
    }
}
