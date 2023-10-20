namespace BankAdministration.Data;

using BankAdministration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public class DataInitializer
{
    private readonly BankContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public DataInitializer(BankContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public void SeedData()
    {
        _context.Database.Migrate();
        //SeedAccounts();
        SeedCountries();
        SeedRoles();
        SeedUsers();
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

    private void SeedUsers()
    {
        AddUserIfNotExists("admin@banken.se", "Hejsan123#", new string[] { "Admin" });
        AddUserIfNotExists("cashier@banken.se", "Hejsan123#", new string[] { "Cashier" });
    }

    private void SeedRoles()
    {
        AddRoleIfNotExisting("Admin");
        AddRoleIfNotExisting("Cashier");
    }
    private void AddRoleIfNotExisting(string roleName)
    {
        var role = _context.Roles.FirstOrDefault(r => r.Name == roleName);
        if (role == null)
        {
            _context.Roles.Add(new IdentityRole { Name = roleName, NormalizedName = roleName });
            _context.SaveChanges();
        }
    }

    public void AddUserIfNotExists(string userName, string password, string[] roles)
    {
        if (_userManager.FindByEmailAsync(userName).Result != null) return;

        var user = new IdentityUser
        {
            UserName = userName,
            Email = userName,
            EmailConfirmed = true
        };
        _userManager.CreateAsync(user, password).Wait();
        _userManager.AddToRolesAsync(user, roles).Wait();
    }
}

