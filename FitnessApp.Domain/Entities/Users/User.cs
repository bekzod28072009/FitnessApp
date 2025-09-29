using FitnessApp.Domain.Common;
using FitnessApp.Domain.Entities.Auth;
using FitnessApp.Domain.Enums;

namespace FitnessApp.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Password { get; set; }
    public string AvatarUrl { get; set; }
    public UserStatus UserStatus { get; set; }
    public Guid RolesId { get; set; }
    public Role Roles { get; set; }
}
