using System.Collections.Generic;
using System.Web;
using System;
using System.ComponentModel;

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
    public string RecipeInstructions { get; set; }
    public string RecipeIngredients { get; set; }
    public string RecipeRating { get; set; }
    public ICollection<CategoryRecipe> JoinEntries { get; }

  }
}