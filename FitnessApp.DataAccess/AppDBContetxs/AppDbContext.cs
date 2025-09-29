using FitnessApp.Domain.Entities.Auth;
using FitnessApp.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.DataAccess.AppDBContetxs;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<Token> Tokens { get; set; }

}
