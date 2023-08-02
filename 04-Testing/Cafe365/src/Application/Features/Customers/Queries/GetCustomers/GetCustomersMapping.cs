using Cafe365.Domain.Customers;

namespace Cafe365.Application.Features.Customers.Queries.GetCustomers;

public class GetCustomersMapping : Profile
{
    public GetCustomersMapping()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(d => d.CustomerId, opt => opt.MapFrom(s => s.Id.Value));

        CreateMap<Address, AddressDto>();
    }
}