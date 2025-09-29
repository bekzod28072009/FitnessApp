using FitnessApp.Domain.Common;
using FitnessApp.Domain.Entities.Users;

namespace FitnessApp.Domain.Entities.Auth;

public class Token : Auditable
{
    public Guid UsersId { get; set; }
    public User Users { get; set; }
    public string RefreshToken { get; set; }
    public string AccessToken { get; set; }
    public string? IpAddress { get; set; }
    public DateTime ExpiredRefreshTokenDate { get; set; }
    public DateTime ExpiredAccessTokenDate { get; set; }
}
