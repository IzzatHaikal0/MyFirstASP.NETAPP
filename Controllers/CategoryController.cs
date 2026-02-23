using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Data;
using System.Linq;

namespace MyMvcApp.Controllers
{
    public class CategoryController : Controller
    {
        // GET: CategoryController
        public IActionResult Viewcategory()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            var Category = new Category { CategoryId = id.HasValue?id.Value:0}; //create instance of model

            return View(Category);
        }
    }
}
