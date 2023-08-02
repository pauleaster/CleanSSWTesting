using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FluentValidation.Validators;
using Cafe365.Melbourne.Domain.TodoItems;
using Bogus;
using Cafe365.Melbourne.Domain.Common.ValueObjects;
using Cafe365.Melbourne.Domain.Products;
using Cafe365.Melbourne.Domain.Orders;
using Bogus.DataSets;
using Cafe365.Melbourne.Domain.Customers;

namespace Cafe365.Melbourne.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _dbContext;

    public const int NumProducts = 20;
    private const int NumCustomers = 20;
    private const int NumOrders = 20;
    private const int NumOrderItems = 5;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext dbContext)
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
                //await _dbContext.Database.MigrateAsync();
                await _dbContext.Database.EnsureDeletedAsync();
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

        var productFaker = new Faker<Product>()
            .CustomInstantiator(f => new Product
            {
                Name = f.Commerce.ProductName(),
                Price = moneyFaker.Generate(),
                Sku = f.Commerce.Ean13(),
                AvailableStock = f.Random.Int(0, 100)
            });

        var products = productFaker.Generate(NumProducts);
        _dbContext.Products.AddRange(products);
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

    private async Task SeedCustomersAsync()
    {
        if (await _dbContext.Customers.AnyAsync())
            return;

        var customerFaker = new Faker<Customer>()
            .CustomInstantiator(f => new Customer
            {
                FirstName = f.Person.FirstName,
                LastName = f.Person.LastName,
                Email = f.Person.Email,
                Phone = f.Person.Phone,
                Address = f.Address.StreetAddress(),
                City = f.Address.City(),
                State = f.Address.State(),
                Country = f.Address.Country(),
                PostCode = f.Address.ZipCode()
            });

        var customers = customerFaker.Generate(NumCustomers);
        _dbContext.Customers.AddRange(customers);
        await _dbContext.SaveChangesAsync();
    }
}