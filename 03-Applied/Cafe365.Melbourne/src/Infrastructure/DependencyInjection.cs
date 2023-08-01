using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cafe365.Melbourne.Application.Common.Interfaces;
using Cafe365.Melbourne.Infrastructure.Persistence;
using Cafe365.Melbourne.Infrastructure.Persistence.Interceptors;
using Cafe365.Melbourne.Infrastructure.Services;
using Cafe365.Melbourne.Infrastructure.Email;
using Cafe365.Melbourne.Infrastructure.Payments;

namespace Cafe365.Melbourne.Infrastructure;

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