using Cafe365.Melbourne.Domain.Orders;

namespace Cafe365.Melbourne.Application.Common.Interfaces;

public interface IEmailProvider
{
    public Task SendConfirmationEmail(string email, string customerFirstName, Order order);
}
