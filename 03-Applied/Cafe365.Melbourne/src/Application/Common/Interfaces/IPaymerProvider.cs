using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe365.Melbourne.Application.Common.Interfaces;

public interface IPaymentProvider
{
    public Task<PaymentResult> ProcessPayment(Payment payment);
}

public record Payment(string CardNumber, string CardName, string CVV, string ExpiryMonth, string ExpiryYear, decimal Amount, string Currency);

public record PaymentResult(bool IsSuccessful, string? Error);
