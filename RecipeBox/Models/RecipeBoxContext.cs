using Microsoft.EntityFrameworkCore;
//Identifying the Database Schema

namespace RecipeBox.Models
{
  public class RecipeBoxContext : DbContext
  {
    public virtual DbSet<Category> Categories { get; set; } //DBSets are new tables being created. 
    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<CategoryRecipe> CategoryRecipe{ get; set; }

    public RecipeBoxContext(DbContextOptions options) : base(options) { } 
  }
}