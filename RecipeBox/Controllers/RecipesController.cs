using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System;



namespace RecipeBox.Controllers
{
  [Authorize]
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public RecipesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    // public async Task<ActionResult> Index(string sortOrder, string searchString)
    public ViewResult Index(string sortOrder, string searchString)
    {
      // var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      // var currentUser = await _userManager.FindByIdAsync(userId);
      // var userRecipes = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id).ToList();
      // return View(userRecipes);
    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "recipe_desc" : "";
    ViewBag.RatingSortParm = sortOrder == "Rating" ? "rating_desc" : "Rating";
    var recipes = from s in _db.Recipes
    select s;
    if (!String.IsNullOrEmpty(searchString))
    {
        recipes = recipes.Where(s => s.RecipeIngredients.Contains(searchString)
        || s.RecipeIngredients.Contains(searchString));
    }
    switch (sortOrder)
    {
        case "ingt_desc":
            recipes = recipes.OrderByDescending(s => s.RecipeIngredients);
            break;
        case "rate_desc":
            recipes = recipes.OrderBy(s => s.RecipeRating);
            break;
        case "name_desc":
            recipes = recipes.OrderByDescending(s => s.RecipeName);
            break;
        default:
            recipes = recipes.OrderBy(s => s.RecipeIngredients);
            break;
        }
        return View(recipes.ToList());
    
    }




    [HttpPost]
    public async Task<ActionResult> Search(string search)
      {
        var thisUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var thisCurrentUser = await _userManager.FindByIdAsync(thisUserId);
        var userRecipes = _db.Recipes.Where(entry => entry.User.Id == thisCurrentUser.Id);
        var searchedUserRecipes = userRecipes.Where(recipe => (recipe.RecipeIngredients.Contains(search))).ToList();
        return View(searchedUserRecipes);
    }


    public ActionResult Create()
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "CategoryName");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Recipe recipe, int CategoryId)
    {
      Console.WriteLine("Hello");
      Console.WriteLine(recipe.RecipeRating);
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      recipe.User = currentUser;
      _db.Recipes.Add(recipe);
      if (CategoryId != 0)
      {
        _db.CategoryRecipe.Add(new CategoryRecipe() { CategoryId = CategoryId, RecipeId = recipe.RecipeId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisRecipe = _db.Recipes
          .Include(recipe => recipe.JoinEntries)
          .ThenInclude(join => join.Category)
          .FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }


    public ActionResult Edit(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "CategoryName");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult Edit(Recipe recipe, int CategoryId)
    {
      if (CategoryId != 0)
      {
        _db.CategoryRecipe.Add(new CategoryRecipe() { CategoryId = CategoryId, RecipeId = recipe.RecipeId });
      }
      _db.Entry(recipe).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCategory(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "CategoryName");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult AddCategory(Recipe recipe, int CategoryId)
    {
      if (CategoryId != 0)
      {
        _db.CategoryRecipe.Add(new CategoryRecipe() { CategoryId = CategoryId, RecipeId = recipe.RecipeId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteCategory(int joinId)
    {
      var joinEntry = _db.CategoryRecipe.FirstOrDefault(entry => entry.CategoryRecipeId == joinId);
      _db.CategoryRecipe.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}
