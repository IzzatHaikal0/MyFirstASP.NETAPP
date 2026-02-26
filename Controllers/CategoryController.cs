using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Data;
using System.Linq;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryServices;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryServices = categoryService;
        }
        // GET: CategoryController
        public IActionResult Index()
        {
            var CategoryList = _categoryServices.GetAllCategories();
            return View(CategoryList);
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
            _categoryServices.CreateCategory(category);//here we call the category service

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            var Category = _categoryServices.GetCategoryById(id); 

            if(Category == null)
            {
                return NotFound();
            }

            return View(Category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _categoryServices.UpdateCategory(category);

            return RedirectToAction("Index");
        }

    
        public IActionResult Delete(int id)
        {
            var categoryToDelete = _categoryServices.GetCategoryById(id);
            
            if(categoryToDelete != null)
            {
                _categoryServices.DeleteCategory(categoryToDelete);
            }

            return RedirectToAction("Index");
        }
    }
}
