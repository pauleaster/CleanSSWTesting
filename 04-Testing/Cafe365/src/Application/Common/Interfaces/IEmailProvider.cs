using Cafe365.Domain.Orders;

namespace Cafe365.Application.Common.Interfaces;

public interface IEmailProvider
{
    public Task SendConfirmationEmail(string email, string customerFirstName, Order order);
}
