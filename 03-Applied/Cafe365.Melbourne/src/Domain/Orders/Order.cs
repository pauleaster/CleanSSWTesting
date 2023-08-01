using Cafe365.Melbourne.Domain.Common.Base;
using Cafe365.Melbourne.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe365.Melbourne.Domain.Orders;
public class Order : BaseEntity<Guid>
{
    public required Guid CustomerId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal PaidTotal { get; set; }

    public Customer? Customer { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
