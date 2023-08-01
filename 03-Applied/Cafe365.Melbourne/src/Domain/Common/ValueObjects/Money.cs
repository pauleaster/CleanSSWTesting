using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe365.Melbourne.Domain.Common.ValueObjects;

public record Money(string Currency, decimal Amount);
