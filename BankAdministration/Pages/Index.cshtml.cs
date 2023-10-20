namespace BankAdministration.Pages;

using BankAdministration.Models;
using BankAdministration.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly ICustomerService _customerService;

    public class Item
    {
        //Personuppgifter
        public int CustomerId { get; set; }
        public string Gender { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string NationalId { get; set; }
        public DateTime Birthday { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }
        public bool IsActive { get; set; }

        public Country Country { get; set; }
        //Kontouppgifter
        public string Type { get; set; }
        public int AccountId { get; set; }

        public decimal Balance { get; set; }
        public DateTime Created { get; set; }
        public string Frequency { get; set; }
    }
    public List<Item> Items { get; set; }

    //Pagination
    public int CurrentPage { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
    public string SearchWord { get; set; }
    public int PageCount { get; set; }

    //Statistics

    public string ActiveCustomers { get; set; }
    public string AvailableAccounts { get; set; }
    public string SumOfBalances { get; set; }

    public int CustomerId { get; set; }
    public int AccountId { get; set; }

    public IndexModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public void OnGet(int customerId, int accountId, string sortColumn, string sortOrder, int pageno, string searchWord)
    {
        SortColumn = sortColumn;
        SortOrder = sortOrder;
        SearchWord = searchWord;
        if (pageno == 0)
        {
            pageno = 1;
        }
        CurrentPage = pageno;
        AccountId = accountId;
        CustomerId = customerId;
        var pageResult = _customerService.ListCustomers(customerId, sortColumn, sortOrder, CurrentPage, searchWord);
        PageCount = pageResult.PageCount;
        Items = pageResult.Results.Select(e => new Item
        {
            CustomerId = e.CustomerId,
            Gender = e.Gender,
            //AccountId = e.AccountId,
            Givenname = e.Givenname,
            Surname = e.Surname,
            Streetaddress = e.Streetaddress,
            Zipcode = e.Zipcode,
            City = e.City,
            Country = e.Country,
            NationalId = e.NationalId,
            Birthday = (DateTime)e.Birthday,
            Telephonecountrycode = e.Telephonecountrycode,
            Telephonenumber = e.Telephonenumber,
            Emailaddress = e.Emailaddress,
            IsActive = (bool)e.IsActive,
            //Created = e.Account.Created,
            //Balance = e.Account.Balance,
            //Type = e.Type
        }).ToList();
    }
}