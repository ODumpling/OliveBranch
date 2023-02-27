using OliveBranch.Application.Common.Mappings;
using OliveBranch.Domain.Entities;

namespace OliveBranch.Application.Common.Models;

// Note: This is currently just used to demonstrate applying multiple IMapFrom attributes.
public class LookupDto : IMapFrom<TodoList>, IMapFrom<TodoItem>
{
    public int Id { get; set; }

    public string? Title { get; set; }
}