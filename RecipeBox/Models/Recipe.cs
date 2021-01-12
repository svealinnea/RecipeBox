using System.Collections.Generic;
using System.Web.Mvc;

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
    public List<SelectListItem> Rating { get; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1" },
        new SelectListItem { Value = "2", Text = "2" },
        new SelectListItem { Value = "3", Text = "3"  },
        new SelectListItem { Value = "4", Text = "4"  },
        new SelectListItem { Value = "5", Text = "5"  },
    };    
    public ICollection<CategoryRecipe> JoinEntries { get; }
  }
}  