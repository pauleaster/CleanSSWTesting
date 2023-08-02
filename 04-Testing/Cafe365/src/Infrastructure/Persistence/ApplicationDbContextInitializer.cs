using Bogus;
using Cafe365.Domain.Common.ValueObjects;
using Cafe365.Domain.Customers;
using Cafe365.Domain.Orders;
using Cafe365.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cafe365.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _dbContext;

    private const int NumProducts = 20;
    private const int NumCustomers = 20;
    private const int NumOrders = 20;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger,
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_dbContext.Database.IsSqlServer())
            {
                //await _dbContext.Database.EnsureDeletedAsync();
                await _dbContext.Database.EnsureCreatedAsync();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while migrating or initializing the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        await SeedProductsAsync();
        await SeedCustomersAsync();
        await SeedOrdersAsync();
    }

    private async Task SeedProductsAsync()
    {
        if (await _dbContext.Products.AnyAsync())
            return;

        var moneyFaker = new Faker<Money>()
            .CustomInstantiator(f => new Money(f.Finance.Currency().Code, f.Finance.Amount()));

        var faker = new Faker<Product>()
            .CustomInstantiator(f => Product.Create(
                f.Commerce.Ean13(),
                f.Commerce.ProductName(),
                moneyFaker.Generate(),
                f.Random.Int(0, 10)));

        var products = faker.Generate(NumProducts);
        _dbContext.Products.AddRange(products);
        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedCustomersAsync()
    {
        if (await _dbContext.Customers.AnyAsync())
            return;

        var customerFaker = new Faker<Customer>()
            .CustomInstantiator(f => new Customer
            {
                Id = new CustomerId(Guid.NewGuid()),
                FirstName = f.Person.FirstName,
                LastName = f.Person.LastName,
                Email = f.Person.Email,
                Phone = f.Person.Phone,
                Address = new Address
                {
                    Line1 = f.Address.StreetAddress(),
                    Line2 = f.Address.SecondaryAddress(),
                    City = f.Address.City(),
                    State = f.Address.State(),
                    Country = f.Address.Country(),
                    PostCode = f.Address.ZipCode()
                }
            });

        var customers = customerFaker.Generate(NumCustomers);
        _dbContext.Customers.AddRange(customers);
        await _dbContext.SaveChangesAsync();
    }


    private async Task SeedOrdersAsync()
    {
        if (await _dbContext.Orders.AnyAsync())
            return;

        var customerIds = await _dbContext.Customers.Select(c => c.Id).ToListAsync();

        var products = await _dbContext.Products.ToListAsync();

        var moneyFaker = new Faker<Money>()
            .CustomInstantiator(f => new Money(f.Finance.Currency().Code, f.Finance.Amount()));

        var orderFaker = new Faker<Order>()
            .CustomInstantiator(f =>
            {
                var order = Order.Create(f.PickRandom(customerIds));
                order.AddItem(f.PickRandom(products), f.Random.Int(0, 5));
                return order;
            });

        var orders = orderFaker.Generate(NumOrders);
        _dbContext.Orders.AddRange(orders);
        await _dbContext.SaveChangesAsync();
    }
}
