namespace BankAdministration.Models;

public partial class Country
{
    public Country()
    {
        Customers = new HashSet<Customer>();
    }

    public int Id { get; set; }
    public string CountryCode { get; set; }
    public string CountryName { get; set; }

    public virtual ICollection<Customer> Customers { get; set; }
}
