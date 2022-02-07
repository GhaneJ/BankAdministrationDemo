using BankAdministration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAdministration.Pages.User
{
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
}
