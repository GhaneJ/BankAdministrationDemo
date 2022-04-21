using BankAdministration.Infrastructure.Paging;
using BankAdministration.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BankAdministration.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly BankContext _context;
        private readonly IAccountService _accountService;

        public string Gender { get; set; }
        [StringLength(100)]
        public string Givenname { get; set; }
        [StringLength(100)]
        public string Surname { get; set; }
        [StringLength(100)]
        public string Streetaddress { get; set; }
        [StringLength(10)]
        public string Zipcode { get; set; }
        [StringLength(50)]
        [Required]
        public string City { get; set; }

        public DateTime BirthDay { get; set; }
        public string Telephonenumber { get; set; }
        [StringLength(150)]
        [EmailAddress]
        public string Emailaddress { get; set; }
        public string Telephonecountrycode { get; set; }
        public string NationalId { get; set; }
        public int CountryId { get; set; }
        public int AccountId { get; set; }
        public Country Country { get; set; }
        public Country CountryCode { get; set; }
        public string Frequency { get; set; }
        public decimal Balance { get; set; }
        public DateTime Created { get; set; }
        public Account Account { get; set; }
        public Customer Customer { get; set; }
        public string AccType { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public CustomerService(BankContext context)
        {
            _context = context;
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Include(e => e.Dispositions).First(e => e.CustomerId == id);
        }
        public void Update(Customer customer)
        {
            _context.SaveChanges();
        }
        public List<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer CreateCustomer(string gender, string givenname, string surname, string streetaddress, string emailaddress, string city, string zipcode, DateTime birthday, string telephonenumber, string telephonecountrycode, string nationalId, int countryId, bool isactive)
        {
            var person = new Customer
            {
                Gender = gender,
                Givenname = givenname,
                Surname = surname,
                Streetaddress = streetaddress,
                Emailaddress = emailaddress,
                City = city,
                Country = _context.Countries.First(r => r.Id == countryId),
                //CountryCode = _context.Countries.First(r => r.CountryCode == countrycode),
                Zipcode = zipcode,
                Birthday = birthday,
                Telephonenumber = telephonenumber,
                Telephonecountrycode = telephonecountrycode,
                NationalId = nationalId,
                IsActive = isactive
            };
            _context.Customers.Add(person);
            Update(person);            
            var customerId = person.CustomerId;
            var customer = _context.Customers.First(i => i.CustomerId == customerId);

            Account account = new Account
            {
                Balance = Balance,
                Created = DateTime.Now,
                Frequency = "Monthly"
            };
            _context.Accounts.Add(account);
            _context.SaveChanges();

            var newAccount = _context.Accounts.First(i => i.AccountId == account.AccountId);

            var disposition = new Disposition
            {
                Type = "Owner",
                CustomerId = customer.CustomerId,
                AccountId = account.AccountId
            };
            _context.Dispositions.Add(disposition);
            _context.SaveChanges();

            return person;
        }
            
        
        public PagedResult<Customer> ListCustomers(int customerId, string sortColumn, string sortOrder, int page, string searchWord)
        {
            

            var query = _context.Customers.AsQueryable();
            

            if (!string.IsNullOrEmpty(searchWord))
            {
                query = query.Where(r => r.CustomerId.ToString().Equals(searchWord)
                || r.Givenname.Equals(searchWord) || r.City.Equals(searchWord) || (r.Givenname + " " + r.Surname).Equals(searchWord));

                //query = query.Where(r => r.Customers.Givenname.Equals(searchWord) || r.Customers.City.Equals(searchWord));

            }
            //By default, the result is sorted by Givenname and is ascending
            if (string.IsNullOrEmpty(sortColumn))
            {
                sortColumn = "CustomerId";
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "asc";
            }
            if (sortColumn == "CustomerId")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(e => e.CustomerId);
                else
                    query = query.OrderBy(e => e.CustomerId);
            }
            if (sortColumn == "NationalId")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(e => e.NationalId);
                else
                    query = query.OrderBy(e => e.NationalId);
            }
            if (sortColumn == "Givenname")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(e => e.Givenname);
                else
                    query = query.OrderBy(e => e.Givenname);
            }
            if (sortColumn == "Givenname")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(e => e.Givenname);
                else
                    query = query.OrderBy(e => e.Givenname);
            }
            if (sortColumn == "Streetaddress")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(e => e.Streetaddress);
                else
                    query = query.OrderBy(e => e.Streetaddress);
            }
            if (sortColumn == "City")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(e => e.City);
                else
                    query = query.OrderBy(e => e.City);
            }

            return query.Where(e => e.IsActive == true).GetPaged(page, 30); //5 is the pagesize
        }

        public PagedResult<Customer> ListInactiveCustomers(int customerId, string sortColumn, string sortOrder, int page, string searchWord)
        {
            var query = _context.Customers.AsQueryable();
            if (!string.IsNullOrEmpty(searchWord))
            {
                query = query.Where(r => r.Givenname.Contains(searchWord) ||
                r.Streetaddress.Contains(searchWord) || r.City.Contains(searchWord));
            }
            if (string.IsNullOrEmpty(sortColumn))
            {
                sortColumn = "Givenname";
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "asc";
            }
            if (sortColumn == "Givenname")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(p => p.Givenname);
                else
                    query = query.OrderBy(p => p.Givenname);
            }
            if (sortColumn == "streetaddress")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(p => p.Streetaddress);
                else
                    query = query.OrderBy(p => p.Streetaddress);
            }
            if (sortColumn == "City")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(p => p.City);
                else
                    query = query.OrderBy(p => p.City);
            }

            return query.Where(r => r.IsActive == false).GetPaged(page, 25); //5 is the pagesize
        }
    }
}