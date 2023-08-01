using Cafe365.Melbourne.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cafe365.Melbourne.Infrastructure.Payments;

public class MockPaymentProvider : IPaymentProvider
{
    private readonly ILogger<MockPaymentProvider> _logger;

    public MockPaymentProvider(ILogger<MockPaymentProvider> logger)
    {
        _logger = logger;
    }

    public Task<PaymentResult> ProcessPayment(Payment payment)
    {
        _logger.LogInformation("Processing payment - {payment}", payment);
        var result = new PaymentResult(true, null);
        return Task.FromResult(result);
    }
}
