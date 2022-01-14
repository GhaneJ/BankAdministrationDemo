using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAdministration.Pages
{
    //public class CustomersModel : PageModel
    //{
    //    private readonly BankContext _context;
    //    public CustomersModel(BankContext context)
    //    {
    //        _context = context;
    //    }

    //    public int Id { get; set; }
    //    public string Givenname { get; set; }
    //    public string Surname { get; set; }
    //    public string Address { get; set; }
    //    public string City { get; set; }
    //    public string Zipcode { get; set; }
    //    public string Country { get; set; }
    //    public string Email { get; set; }
    //    public List<Customer> Customers { get; set; }

    //    public void OnGet(string sortColumn, string sortOrder)
    //    {

    //        var query = _context.Customers.Where(r => r.Active == true).Select(r => new Customer
    //        {
    //            CustomerId = r.CustomerId,
    //            Givenname = r.Givenname,
    //            Surname = r.Surname,
    //            Streetaddress = r.Streetaddress,
    //            City = r.City,
    //            Zipcode = r.Zipcode,
    //            Country = r.Country,
    //            Emailaddress = r.Emailaddress
    //        });
    //        if (sortColumn == "Givenname")
    //        {
    //            if (sortOrder == "asc")

    //                query = query.OrderBy(r => r.Givenname);

    //            else

    //                query = query.OrderByDescending(r => r.Givenname);
    //        }

    //        else if (sortColumn == "Streetaddress")
    //        {
    //            if (sortOrder == "asc")

    //                query = query.OrderBy(r => r.Streetaddress);

    //            else

    //                query = query.OrderByDescending(r => r.Streetaddress);
    //        }

    //        else if (sortColumn == "Zipcode")
    //        {
    //            if (sortOrder == "asc")

    //                query = query.OrderBy(r => r.Zipcode);

    //            else

    //                query = query.OrderByDescending(r => r.Zipcode);

    //        }
    //        else if (sortColumn == "City")
    //        {
    //            if (sortOrder == "asc")

    //                query = query.OrderBy(r => r.City);

    //            else

    //                query = query.OrderByDescending(r => r.City);

    //        }
    //        else if (sortColumn == "Country")
    //        {
    //            if (sortOrder == "asc")

    //                query = query.OrderBy(r => r.Country);

    //            else

    //                query = query.OrderByDescending(r => r.Country);

    //        }
    //        else if (sortColumn == "Emailaddress")
    //        {
    //            if (sortOrder == "asc")

    //                query = query.OrderBy(r => r.Emailaddress);

    //            else

    //                query = query.OrderByDescending(r => r.Emailaddress);

    //        }

    //        Customers = query.ToList();
    //    }
    //}

    public class CustomersModel : PageModel
    {
        private readonly IStatisticsService _statisticsService;
        private readonly ICustomerService _customerService;

        public class Item
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
        }


        //Pagination
        public int CurrentPage { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string SearchWord { get; set; }
        public int PageCount { get; set; }

        //Statistics
        public int CustomerId { get; set; }
        public List<Item> Items { get; set; }

        public CustomersModel(IStatisticsService statisticsService, ICustomerService customerService)
        {
            _statisticsService = statisticsService;
            _customerService = customerService;
        }

        public void OnGet(int customerId, string sortColumn, string sortOrder, int pageno, string searchWord)
        {
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            SearchWord = searchWord;
            if (pageno == 0)
            {
                pageno = 1;
            }
            CurrentPage = pageno;
            CustomerId = customerId;
            var pageResult = _customerService.ListCustomers(customerId, sortColumn, sortOrder, CurrentPage, searchWord);
            PageCount = pageResult.PageCount;
            Items = pageResult.Results.Select(e => new Item
            {
                Id = e.CustomerId,
                FirstName = e.Givenname,
                LastName = e.Surname,
                Address = e.Streetaddress,
                City = e.City
            }).ToList();
        }
    }
}
