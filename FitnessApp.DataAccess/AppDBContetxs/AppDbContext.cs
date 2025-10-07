using FitnessApp.Domain.Entities.Auth;
using FitnessApp.Domain.Entities.Nutrition;
using FitnessApp.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.DataAccess.AppDBContetxs;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    //Auth
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<Token> Tokens { get; set; }


    //Nutrition
    public virtual DbSet<FoodItem> FoodItems { get; set; }
    public virtual DbSet<MealItem> MealItems { get; set; }
    public virtual DbSet<Meal> Meals { get; set; }
    public virtual DbSet<MealPlan> MealPlans { get; set; }
    public virtual DbSet<UserMealLog> UserMealLogs { get; set; }

}
