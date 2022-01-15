using System;
using System.Collections.Generic;

namespace BankAdministration.Models
{
    public partial class Country
    {
        public Country()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string CountryCode { get; set; } = null!;
        public string CountryName { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
