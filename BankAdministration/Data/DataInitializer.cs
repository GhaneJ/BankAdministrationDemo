using BankAdministration.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAdministration.Data
{
    public class DataInitializer
    {
        private readonly BankContext _Context;
        public DataInitializer(BankContext context)
        {
            _Context = context;
        }
        public void SeedData()
        {
            _Context.Database.Migrate();
            SeedAccounts();
        }

        private void SeedAccounts()
        {
            AddAccountIfNotExists(12345);
            AddAccountIfNotExists(55555);
        }

        private void AddAccountIfNotExists(int accountId)
        {
            if (_Context.Accounts.Any(e => e.AccountId == accountId)) return;
            _Context.Accounts.Add(new Account
            {
                AccountId = accountId,
                Balance = 1000
            });
            _Context.SaveChanges();
        }
    }
}
