using FitnessApp.Domain.Common;
using FitnessApp.Domain.Entities.Users;

namespace FitnessApp.Domain.Entities.Auth;

public class Role : Auditable
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<Permission>? Permissions { get; set; }
    public List<User>? Users { get; set; }

    public Role()
    {
        Permissions = new List<Permission>();
        Users = new List<User>();
    }
}
