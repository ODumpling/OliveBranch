using OliveBranch.Domain.Common;
using OliveBranch.Domain.Entities;

namespace OliveBranch.Domain.Events;

public class TodoItemCompletedEvent : BaseEvent
{
    public TodoItemCompletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}