using Cafe365.Application.Common.Interfaces;
using Cafe365.Infrastructure.Persistence;
using GraphQL.Filters;
using GraphQL.Mutations;
using GraphQL.Queries;

namespace GraphQL;

public static class DependencyInjection
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services)
    {
        services
            .AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddHttpContextAccessor();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddCors();

        services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            .RegisterDbContext<ApplicationDbContext>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddErrorFilter<ValidationFilter>()
            ;

        return services;
    }
}
