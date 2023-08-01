using Cafe365.Melbourne.Application.Common.Interfaces;
using Cafe365.Melbourne.Infrastructure.Persistence;
using Cafe365.Melbourne.WebApi.Services;

namespace Cafe365.Melbourne.WebApi;

public static class DependencyInjection
{
    public static void AddWebApi(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddOpenApiDocument(configure => configure.Title = "CleanArchitecture API");

        services.AddEndpointsApiExplorer();
    }
}