namespace BankAdministration.Services;

using BankAdministration.Models;

public interface ICountryService
{
    public Country GetCountry(int countryId);
    public void FillCountryList();
}
