using Cafe365.Melbourne.Domain.Entities;

namespace Cafe365.Melbourne.Application.Features.EntityNames.Queries.QueryName;

public class QueryNameMapping : Profile
{
    public QueryNameMapping()
    {
        CreateMap<EntityName, EntityNameDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
    }
}
