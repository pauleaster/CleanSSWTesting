using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Cafe365.Melbourne.Application.Common.Interfaces;
using Cafe365.Melbourne.Infrastructure.Persistence;

namespace Cafe365.Melbourne.Application.IntegrationTests.TestHelpers;

internal class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>
{
    public DatabaseContainer Database { get; }

    public IntegrationTestWebApplicationFactory()
    {
        Database = new DatabaseContainer();
    }

    protected override void ConfigureWebHost(IWebHostBuilder webHostBuilder)
    {
        webHostBuilder.ConfigureTestServices(services => services
                .RemoveAll<DbContextOptions<ApplicationDbContext>>()
                .RemoveAll<ApplicationDbContext>()
                .AddDbContext<IApplicationDbContext, ApplicationDbContext>((_, options) =>
                    options.UseSqlServer(
                        Database.ConnectionString,
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))));
    }
}