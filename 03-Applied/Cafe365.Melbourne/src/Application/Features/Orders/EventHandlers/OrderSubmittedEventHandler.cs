using Cafe365.Melbourne.Application.Common.Interfaces;
using Cafe365.Melbourne.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cafe365.Melbourne.Application.Features.Orders.EventHandlers;


public class OrderSubmittedEventHandler : INotificationHandler<OrderSubmittedEvent>
{
    private readonly ILogger<OrderSubmittedEventHandler> _logger;
    private readonly IEmailProvider _emailProvider;
    private readonly IApplicationDbContext _dbContext;

    public OrderSubmittedEventHandler(ILogger<OrderSubmittedEventHandler> logger, IEmailProvider emailProvider, IApplicationDbContext dbContext)
    {
        _logger = logger;
        _emailProvider = emailProvider;
        _dbContext = dbContext;
    }

    public async Task Handle(OrderSubmittedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(OrderSubmittedEvent)} event handled");

        var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == notification.Order.CustomerId, cancellationToken);
        if (customer is null)
            throw new ValidationException("Could not find customer");

        await _emailProvider.SendConfirmationEmail(customer.Email, customer.FirstName, notification.Order);
    }
}
