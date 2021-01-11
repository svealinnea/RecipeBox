using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Controllers
{
  public class CategoriesController : Controller // allows ParentsController to operate as a Controller
  {
    private readonly RecipeBoxContext _db; // Defining the Database as Template
    public CategoriesController(RecipeBoxContext db) //constructor for the controller 
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Category> model = _db.Categories.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Category category)
    {
      _db.Categories.Add(category);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisCategory = _db.Categories //return Parent name and id 
          .Include(category => category.JoinEntries) //find childs(JoinEntries) related to the parent
          .ThenInclude(join => join.Recipe) //With all join entries add the related child 
          .FirstOrDefault(category => category.CategoryId == id); // find the Parent that matches the ID
      return View(thisCategory);
    }

    public ActionResult Edit(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id); // finds the first match and assigns it to "thisParent".
      return  View(thisCategory);
    }

    [HttpPost]
    public ActionResult Edit(Category category) //parent is an object that contains all properties, not just the ID
    {
      _db.Entry(category).State = EntityState.Modified; // holding the information in a bucket
      _db.SaveChanges();// pour the bucket into the database
      return RedirectToAction("Index"); //returning to index page in parents
    }

    public ActionResult Delete(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category=> category.CategoryId == id);
      return View(thisCategory);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category=> category.CategoryId == id);
      _db.Categories.Remove(thisCategory);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}