using OliveBranch.Domain.Common;
using OliveBranch.Domain.ValueObjects;

namespace OliveBranch.Domain.Entities;

public class TodoList : BaseAuditableEntity
{
    public string? Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;

    public IList<TodoItem> Items { get; } = new List<TodoItem>();

    public virtual ApplicationUser User { get; set; }
}