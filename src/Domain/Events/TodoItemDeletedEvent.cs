using OliveBranch.Domain.Common;
using OliveBranch.Domain.Entities;

namespace OliveBranch.Domain.Events;

public class TodoItemDeletedEvent : BaseEvent
{
    public TodoItemDeletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}