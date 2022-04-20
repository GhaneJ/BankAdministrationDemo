using BankAdministration.Models;
using BankAdministration.Services.CustomerService.GeoLocation;

namespace BankAdministration.Services.CustomerService
{
    public class LocationService : ILocationService
    {
        private readonly BankContext _context;

        public LocationService(BankContext context)
        {
            _context = context;
        }
        public int NumberOfSwedishCustomers()
        {
            return _context.Customers.Where(c => c.CountryId == 1).Count();
        }
        public int NumberOfNorwegianCustomers()
        {
            return _context.Customers.Where(c => c.CountryId == 2).Count();
        }
        public int NumberOfDanishCustomers()
        {
            return _context.Customers.Where(c => c.CountryId == 3).Count();
        }
        public int NumberOfFinnishCustomers()
        {
            return _context.Customers.Where(c => c.CountryId == 4).Count();
        }
    }
}
