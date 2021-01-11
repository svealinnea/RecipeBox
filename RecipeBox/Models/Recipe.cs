using System.Collections.Generic;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public Recipe()
    {
      this.JoinEntries = new HashSet<CategoryRecipe>();
    }

    public int RecipeId { get; set; }
    public string RecipeName { get; set; }

    public ICollection<CategoryRecipe> JoinEntries { get; }
  }
}  