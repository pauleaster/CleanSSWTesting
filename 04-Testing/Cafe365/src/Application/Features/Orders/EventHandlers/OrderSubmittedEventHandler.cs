using Cafe365.Application.Common.Interfaces;
using Cafe365.Domain.Orders;
using Microsoft.Extensions.Logging;

namespace Cafe365.Application.Features.Orders.EventHandlers;

public class OrderSubmittedEventHandler : INotificationHandler<OrderSubmittedEvent>
{
    private readonly ILogger<OrderSubmittedEventHandler> _logger;
    private readonly IEmailProvider _emailProvider;

    public OrderSubmittedEventHandler(ILogger<OrderSubmittedEventHandler> logger, IEmailProvider emailProvider)
    {
        _logger = logger;
        _emailProvider = emailProvider;
    }

    public async Task Handle(OrderSubmittedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(OrderSubmittedEvent)} event handled");

        await _emailProvider.SendConfirmationEmail(notification.Customer.Email, notification.Customer.FirstName, notification.Order);
    }
}
