using Cafe365.Melbourne.Application.Common.Exceptions;
using Cafe365.Melbourne.Application.Features.TodoItems.Commands.CreateTodoItem;
using Cafe365.Melbourne.Application.IntegrationTests.TestHelpers;
using Cafe365.Melbourne.Domain.TodoItems;

namespace Cafe365.Melbourne.Application.IntegrationTests.Features.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandTests : IntegrationTestBase
{
    public CreateTodoItemCommandTests(TestingDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task ShouldRequireUniqueTitle()
    {
        await Mediator.Send(new CreateTodoItemCommand("Shopping"));

        var command = new CreateTodoItemCommand("Shopping");

        await FluentActions.Invoking(() =>
            Mediator.Send(command)).Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ShouldCreateTodoItem()
    {
        var command = new CreateTodoItemCommand("Tasks");

        var id = await Mediator.Send(command);

        var item = (await Context.TodoItems.FindAsync(new TodoItemId(id)))!;

        item.Should().NotBeNull();
        item.Title.Should().Be(command.Title);
        item.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(10));
    }
}