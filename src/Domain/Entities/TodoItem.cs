﻿using Domain.Common;
using Domain.Enums;
using Domain.Events;

namespace Domain.Entities;

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