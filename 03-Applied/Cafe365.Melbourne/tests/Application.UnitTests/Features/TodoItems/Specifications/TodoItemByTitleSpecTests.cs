using Cafe365.Melbourne.Application.Features.TodoItems.Specifications;
using Cafe365.Melbourne.Domain.TodoItems;

namespace Cafe365.Melbourne.Application.UnitTests.Features.TodoItems.Specifications;

public class TodoItemByTitleSpecTests
{
    private readonly List<TodoItem> _entities;

    public TodoItemByTitleSpecTests()
    {
        _entities = new List<TodoItem>()
        {
            TodoItem.Create("Apple"),
            TodoItem.Create("Banana"),
            TodoItem.Create("Apple 2"),
            TodoItem.Create("Banana 2"),
            TodoItem.Create("Hello world 2"),
        };
    }

    [Theory]
    [InlineData("Apple")]
    [InlineData("Banana")]
    public void Should_Return_ByTitle(string textToSearch)
    {
        var query = new TodoItemByTitleSpec(textToSearch);
        var result = query.Evaluate(_entities).ToList();

        result.Count.Should().Be(1);
        result.First().Title.Should().Be(textToSearch);
    }
}