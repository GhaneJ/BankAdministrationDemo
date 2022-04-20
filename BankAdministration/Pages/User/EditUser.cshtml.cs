using BankAdministration.Data;
using BankAdministration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankAdministration.Pages.User
{
    [Authorize(Roles = "Admin")]
    public class EditUserModel : PageModel
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

        public EditUserModel(BankContext context, DataInitializer dataInitializer)
        {
            _context = context;
            _dataInitializer = dataInitializer;
        }        

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                _context.SaveChanges();
                return RedirectToPage("Customers");
            }
            
            return Page();
        }

        public void OnGet(int id)
        {
            var user = _context.Users.Where(r => r.UserId == id);
            
        }
    }
}
