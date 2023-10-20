namespace BankAdministration.Services.CustomerService.GeoLocation;

public interface ILocationService
{
    public int NumberOfSwedishCustomers();
    public int NumberOfNorwegianCustomers();
    public int NumberOfDanishCustomers();
    public int NumberOfFinnishCustomers();
}
