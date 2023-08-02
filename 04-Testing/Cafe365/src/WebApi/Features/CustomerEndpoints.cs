using Cafe365.Application.Features.Customers.Queries.GetCustomers;
using Cafe365.WebApi.Extensions;
using MediatR;

namespace Cafe365.WebApi.Features;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("customers")
            .WithTags("Customers")
            .WithOpenApi();

        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetCustomersQuery(), ct))
            .WithName("GetCustomers")
            .ProducesGet<CustomerDto[]>();
    }
}
