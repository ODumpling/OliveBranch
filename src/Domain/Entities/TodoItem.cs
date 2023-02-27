using OliveBranch.Domain.Common;
using OliveBranch.Domain.Enums;
using OliveBranch.Domain.Events;

namespace OliveBranch.Domain.Entities;

public class TodoItem : BaseAuditableEntity
{
    private bool _done;
    public int ListId { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTime? Reminder { get; set; }

    public bool Done
    {
        get => _done;
        set
        {
            if (value && _done == false) AddDomainEvent(new TodoItemCompletedEvent(this));

            _done = value;
        }
    }

    public TodoList List { get; set; } = null!;
}