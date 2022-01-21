﻿using BankAdministration.Models;
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
        private readonly IAccountService _accountService;


        public class Item
        {
            public int CustomerId { get; set; }
            public string Givenname { get; set; }
            public string Surname { get; set; }
            public string Streetaddress { get; set; }
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
        public int AccountId { get; set; }
        public string ActiveCustomers { get; set; }
        public string AvailableAccounts { get; set; }
        public string SumOfBalances { get; set; }
        public List<Item> Items { get; set; }

        public IndexModel(IStatisticsService statisticsService, IAccountService accountService)
        {
            _statisticsService = statisticsService;
            _accountService = accountService;
        }

        public void OnGet(int accountId, string sortColumn, string sortOrder, int pageno, string searchWord)
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
            //AccountId = accountId;
            var pageResult = _accountService.ListAccounts(accountId, sortColumn, sortOrder, CurrentPage, searchWord);
            PageCount = pageResult.PageCount;
            Items = pageResult.Results.Select(e => new Item
            {
                AccountId = e.AccountId,
                Givenname = e.Customers.Givenname,
                Surname = e.Customers.Surname,
                Streetaddress = e.Customers.Streetaddress,
                City = e.Customers.City,
            }).ToList();
        }
    }
}