using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
  public class Category
  {
    public Category()
    {
      this.JoinEntries = new HashSet<CategoryRecipe>();
    }
    public int CategoryId { get; set; }

    [DisplayName("Category Name")]
    public string CategoryName { get; set; }
    public virtual ICollection<CategoryRecipe> JoinEntries { get; set; }
  }
}

