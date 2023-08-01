using Cafe365.Melbourne.Domain.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe365.Melbourne.Domain.Products;
public class Product : BaseEntity<Guid>
{
    public string Sku { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Currency { get; set; } = null!;
    public int AvailableStock { get; set; }
    public bool IsStockedItem { get; set; }
}
