using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BankAdministration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IStatisticsService _statisticsService;
        private readonly ICustomerService _customerService;


        public class Item
        {
            public int CustomerId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Type { get; set; }
            public int AccountId { get; set; }
            public decimal Balance { get; set; }
            public DateTime Created { get; set; }
            public string Frequency { get; set; }
        }



        //Pagination
        public int CurrentPage { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string SearchWord { get; set; }
        public int PageCount { get; set; }

        //Statistics
        public int CustomerId { get; set; }
        public string ActiveCustomers { get; set; }
        public string AvailableAccounts { get; set; }
        public string SumOfBalances { get; set; }
        public List<Item> Items { get; set; }

        public IndexModel(IStatisticsService statisticsService, ICustomerService customerService)
        {
            _statisticsService = statisticsService;
            _customerService = customerService;
        }

        public void OnGet(int customerId, string sortColumn, string sortOrder, int pageno, string searchWord)
        {
            ActiveCustomers = _statisticsService.activeCustomers();
            AvailableAccounts = _statisticsService.availabAccounts();
            SumOfBalances = _statisticsService.sumOfAccountBalances();

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
                CustomerId = e.CustomerId,
                FirstName = e.Givenname,
                LastName = e.Surname,
                Address = e.Streetaddress,
                City = e.City
            }).ToList();
        }
    }
}