using AutoMapper.QueryableExtensions;
using Cafe365.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cafe365.Application.Features.Customers.Queries.GetCustomers;

public record GetCustomersQuery : IRequest<IReadOnlyList<CustomerDto>>;

public class CustomerDto
{
    public Guid CustomerId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required AddressDto Address { get; init; }
}

public record AddressDto
{
    public required string Line1 { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string PostCode { get; init; }
    public required string Country { get; init; }
}

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IReadOnlyList<CustomerDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public GetCustomersQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Customers
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
