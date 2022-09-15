using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    // GET
    public IActionResult Index()
    {
        IEnumerable<Category> categories = _db.Categories;
        return View(categories);
    }
    
    // GET
    public IActionResult Create()
    {
        return View();
    }
    
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            TempData["success"] = "Category has been successfully created.";
            return RedirectToAction("Index");
        }
        return View(category);
    }
    
    // GET
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0) { return NotFound(); }
        var category = _db.Categories.Find(id);
        if (category == null) { return NotFound(); }
        return View(category);
    }
    
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
            TempData["success"] = "Category has been successfully updated.";
            return RedirectToAction("Index");
        }
        return View(category);
    }
    
    // GET
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0) { return NotFound(); }
        var category = _db.Categories.Find(id);
        if (category == null) { return NotFound(); }
        return View(category);
    }
    
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Category category)
    {
        _db.Categories.Remove(category);
        _db.SaveChanges();
        TempData["success"] = "Category has been successfully deleted.";
        return RedirectToAction("Index");
    }
}