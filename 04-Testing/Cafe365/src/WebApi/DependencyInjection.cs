using Cafe365.Application.Common.Interfaces;
using Cafe365.Infrastructure.Persistence;
using Cafe365.WebApi.Services;

namespace Cafe365.WebApi;

public static class DependencyInjection
{
    public static void AddWebApi(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddOpenApiDocument(configure => configure.Title = "Cafe365 API");

        services.AddEndpointsApiExplorer();
    }
}