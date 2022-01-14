using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAdministration.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Dispositions = new HashSet<Disposition>();
        }

        public int CustomerId { get; set; }
        public string Gender { get; set; } = null!;
        [StringLength(100)]
        public string Givenname { get; set; } = null!;
        [StringLength(100)]
        public string Surname { get; set; } = null!;
        [StringLength(100)]
        public string Streetaddress { get; set; } = null!;
        [StringLength(50)]
        public string City { get; set; } = null!;
        [StringLength(10)]
        public string Zipcode { get; set; } = null!;
        [StringLength(100)]
        public string Country { get; set; } = null!;
        [StringLength(2)]
        public string CountryCode { get; set; } = null!;
        public DateTime? Birthday { get; set; }
        public string? NationalId { get; set; }
        public string? Telephonecountrycode { get; set; }
        public string? Telephonenumber { get; set; }
        [StringLength(150)]
        public string? Emailaddress { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Disposition> Dispositions { get; set; }
    }
}
