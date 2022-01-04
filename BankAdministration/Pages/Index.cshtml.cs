using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAdministration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BankContext _context;
        public IndexModel(BankContext context)
        {
            _context = context;
        }
        //We always create what we're going to show in our razor view,
        //even expressions like count()
        public string availableCustomers { get; set; }
        public List<Customer> Customers { get; set; }
        public void OnGet()
        {
            availableCustomers = _context.Customers.Count().ToString();
        }
    }
}