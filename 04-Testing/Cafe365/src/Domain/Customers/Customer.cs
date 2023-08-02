using Cafe365.Domain.Common.Base;
using Cafe365.Domain.Orders;

namespace Cafe365.Domain.Customers;

public class Customer : Entity<CustomerId>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public Address Address { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

public record CustomerId(Guid Value);
