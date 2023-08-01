using Cafe365.Melbourne.Domain.Common.Base;
using Cafe365.Melbourne.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe365.Melbourne.Domain.Customers;

public record CustomerId(Guid Value);

public class Customer : BaseEntity<CustomerId>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string PostCode { get; set; } = null!;
    public string Country { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
