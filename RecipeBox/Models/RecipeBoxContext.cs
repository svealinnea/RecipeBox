using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RecipeBox.Models
{
  public class RecipeBoxContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Category> Categories { get; set; }
    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<CategoryRecipe> CategoryRecipe { get; set; }

    public RecipeBoxContext(DbContextOptions options) : base(options) { }
  }
}