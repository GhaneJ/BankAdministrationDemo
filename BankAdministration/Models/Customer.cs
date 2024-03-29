﻿namespace BankAdministration.Models;

public partial class Customer
{
    public Customer()
    {
        Dispositions = new HashSet<Disposition>();
    }

    public int CustomerId { get; set; }
    public string Gender { get; set; } = null!;
    public string Givenname { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Streetaddress { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Zipcode { get; set; } = null!;
    public int CountryId { get; set; }
    public DateTime? Birthday { get; set; }
    public string NationalId { get; set; }
    public string Telephonecountrycode { get; set; }
    public string Telephonenumber { get; set; }
    public string Emailaddress { get; set; }
    public bool? IsActive { get; set; }        

    public virtual Country Country { get; set; }
    public virtual ICollection<Disposition> Dispositions { get; set; }
}
