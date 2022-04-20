using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAdministration.Pages
{
    public class CustomersModel : PageModel
    {
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

        public CustomersModel(ICustomerService customerService)
        {
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
