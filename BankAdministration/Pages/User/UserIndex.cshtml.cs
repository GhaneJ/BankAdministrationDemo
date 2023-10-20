namespace BankAdministration.Pages.User;

using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class UserIndexModel : PageModel
{
    private readonly BankContext _context;

    public UserIndexModel(BankContext context)
    {
    }

    public void OnGet()
    {
    }
}
