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
            this.JoinEntries = new HashSet <CategoryRecipe>(); 
        }
        // [DisplayName("Start Date")]
        // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        // public DateTime StartDate { get; set; }
        public int CategoryId { get; set; }

        [DisplayName( "Category Name")]
        public string CategoryName { get; set; }
        public virtual ICollection <CategoryRecipe> JoinEntries { get; set; } 
    }
}

