using Microsoft.AspNetCore.Identity;

namespace OliveBranch.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    private ICollection<TodoList> TodoLists { get; set; }
}