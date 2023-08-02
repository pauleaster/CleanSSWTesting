using Bogus;
using Cafe365.Application.Features.TodoItems.Queries.GetAllTodoItems;
using Cafe365.Application.IntegrationTests.TestHelpers;
using Cafe365.Domain.Entities;

namespace Cafe365.Application.IntegrationTests.Features.TodoItems.Queries.GetAllTodoItems;

public class GetAllTodoItemsQueryTests : IntegrationTestBase
{
    public GetAllTodoItemsQueryTests(TestingDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task Should_Return_All_TodoItems()
    {
        const int entityCount = 10;
        var entities = new Faker<TodoItem>()
            .RuleFor(t => t.Title, f => f.UniqueIndex.ToString())
            .Generate(entityCount);

        await Context.TodoItems.AddRangeAsync(entities);
        await Context.SaveChangesAsync();

        var result = await Mediator.Send(new GetAllTodoItemsQuery());

        result.Count.Should().Be(entityCount);
    }
}