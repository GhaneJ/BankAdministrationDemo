using BankAdministration.Data;
using BankAdministration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankAdministration.Pages.User
{
    [Authorize(Roles = "Admin")]
    public class CreateUserModel : PageModel
    {
        private readonly BankContext _context;
        private readonly DataInitializer _dataInitializer;

        [Required(ErrorMessage = "E-post m�ste anges")]
        [EmailAddress(ErrorMessage = "E-postadressen �r inte giltig")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Det kr�vs att ange ett l�senord")]
        [StringLength(20, ErrorMessage = "L�senordet m�ste vara mellan 5 and 20 tecknen", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }        
        public string Role { get; set; } = "Unspecified";
        public string[] Roles = new[] { "Admin", "Cashier" };
        
        public CreateUserModel(BankContext context, DataInitializer dataInitializer)
        {
            _context = context;
            _dataInitializer = dataInitializer;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost(string userName, string password, string[] roles)
        {
            
            if (ModelState.IsValid)
            {
                _dataInitializer.AddUserIfNotExists(userName, password, roles);

                return RedirectToPage("UserIndex");
            }
            //FillCountryList();
            return Page();
        }
    }
}
