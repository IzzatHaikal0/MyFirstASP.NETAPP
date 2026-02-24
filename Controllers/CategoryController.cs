using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Data;
using System.Linq;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;

namespace MyMvcApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        // GET: CategoryController
        public IActionResult Index()
        {
            var CategoryList = _context.Category.ToList();
            return View(CategoryList);
        }

        public IActionResult Edit(int? id)
        {
            var Category = new Category { CategoryId = id.HasValue?id.Value:0}; //create instance of model

            return View(Category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }
            _context.Category.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var categoryToEdit = _context.Category.Find(id);

            if(categoryToEdit == null)
            {
                return NotFound();
            }

            return View(categoryToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _context.Category.Update(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    
        public IActionResult Delete(int id)
        {
            var categoryToDelete = _context.Category.Find(id);
            
            if(categoryToDelete != null)
            {
                _context.Category.Remove(categoryToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
