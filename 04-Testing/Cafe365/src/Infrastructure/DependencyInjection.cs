using Cafe365.Application.Common.Interfaces;
using Cafe365.Infrastructure.Email;
using Cafe365.Infrastructure.Payments;
using Cafe365.Infrastructure.Persistence;
using Cafe365.Infrastructure.Persistence.Interceptors;
using Cafe365.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cafe365.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<EntitySaveChangesInterceptor>();
        services.AddScoped<DispatchDomainEventsInterceptor>();

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"), builder =>
            {
                builder.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                builder.EnableRetryOnFailure();
            }));

        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddSingleton<IDateTime, DateTimeService>();

        services.AddScoped<IPaymentProvider, MockPaymentProvider>();
        services.AddScoped<IEmailProvider, MockEmailProvider>();

        return services;
    }
}
