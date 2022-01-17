using BankAdministration.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAdministration.Data
{
    public class DataInitializer
    {
        private readonly BankContext _context;
        public DataInitializer(BankContext context)
        {
            _context = context;
        }
        public void SeedData()
        {
            _context.Database.Migrate();
            SeedAccounts();
            SeedCountries();
        }

        private void SeedCountries()
        {
            AddCountryIfNotExists("FI", "Finland");
            AddCountryIfNotExists("SE", "Sverige");
            AddCountryIfNotExists("DK", "Danmark");
            AddCountryIfNotExists("NO", "Norge");
        }

        private void AddCountryIfNotExists(string code, string name)
        {
            if (_context.Countries.Any(r => r.CountryCode == code)) return;
            _context.Countries.Add(new Country { CountryCode = code, CountryName = name });
            _context.SaveChanges();
        }

        private void SeedAccounts()
        {
            AddAccountIfNotExists(12345);
            AddAccountIfNotExists(55555);
        }

        private void AddAccountIfNotExists(int accountId)
        {
            if (_context.Accounts.Any(e => e.AccountId == accountId)) return;
            _context.Accounts.Add(new Account
            {
                AccountId = accountId,
                Balance = 1000
            });
            _context.SaveChanges();
        }
    }
}

