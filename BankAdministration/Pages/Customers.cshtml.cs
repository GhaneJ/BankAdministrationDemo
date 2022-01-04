using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank_Demo.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly BankContext _context;
        public CustomersModel(BankContext context)
        {
            _context = context;
        }

        public int Id { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public List<Customer> Customers { get; set; }

        public void OnGet(string sortColumn, string sortOrder)
        {

            var query = _context.Customers.Where(r => r.Active == true).Select(r => new Customer
            {
                CustomerId = r.CustomerId,
                Givenname = r.Givenname,
                Surname = r.Surname,
                Streetaddress = r.Streetaddress,
                City = r.City,
                Zipcode = r.Zipcode,
                Country = r.Country,
                Emailaddress = r.Emailaddress
            });
            if (sortColumn == "Givenname")
            {
                if (sortOrder == "asc")

                    query = query.OrderBy(r => r.Givenname);

                else

                    query = query.OrderByDescending(r => r.Givenname);
            }

            else if (sortColumn == "Streetaddress")
            {
                if (sortOrder == "asc")

                    query = query.OrderBy(r => r.Streetaddress);

                else

                    query = query.OrderByDescending(r => r.Streetaddress);
            }

            else if (sortColumn == "Zipcode")
            {
                if (sortOrder == "asc")

                    query = query.OrderBy(r => r.Zipcode);

                else

                    query = query.OrderByDescending(r => r.Zipcode);

            }
            else if (sortColumn == "City")
            {
                if (sortOrder == "asc")

                    query = query.OrderBy(r => r.City);

                else

                    query = query.OrderByDescending(r => r.City);

            }
            else if (sortColumn == "Country")
            {
                if (sortOrder == "asc")

                    query = query.OrderBy(r => r.Country);

                else

                    query = query.OrderByDescending(r => r.Country);

            }
            else if (sortColumn == "Emailaddress")
            {
                if (sortOrder == "asc")

                    query = query.OrderBy(r => r.Emailaddress);

                else

                    query = query.OrderByDescending(r => r.Emailaddress);

            }

            Customers = query.ToList();
        }
    }
}
