using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Data; 
using System.Linq;
using Microsoft.VisualBasic;
using SQLitePCL;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using MyMvcApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyMvcApp.Namespace
{
    [Authorize]
    public class SportController : Controller
    {
        //declare db connection    
        private readonly ISportService _sportService;
        private readonly ICategoryService _categoryService;
        
        //connect controller to db
        public SportController(ISportService sportService, ICategoryService categoryService)
        {
            _sportService = sportService;
            _categoryService = categoryService;
        }

        // Display list of sports from db
        public IActionResult Index()
        { 
            var SportsList = _sportService.GetAllSports();
            return View(SportsList);
        }

        //show empty form page to add new sports
        [HttpGet]
        public IActionResult Create()
        {
            var categories = _categoryService.GetAllCategories();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "Name");

            return View();
        }
        
        //process the form and store in db
        [HttpPost]
        public IActionResult Create(Sport sport)
        {
            //validate data receive
            if(!ModelState.IsValid)
            {
                //validate the data, if wrong, send the list back
                var categories = _categoryService.GetAllCategories();
                ViewBag.CategoryId = new SelectList(categories, "CategoryId", "Name");
                //Return the View and send the bad data back so they don't lose what they typed.
                return View(sport);
            }
            _sportService.CreateSport(sport);
        
            return RedirectToAction("Index");
        }

        //delete the sport from db (Select based on id)
        public IActionResult Delete(int id)
        {
            var sportToDelete = _sportService.GetSportById(id);

            if(sportToDelete != null)
            {
                _sportService.DeleteSport(sportToDelete);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //declare variable and fetch the data based on given id
            var sportToEdit = _sportService.GetSportById(id);

            //handle not found
            if(sportToEdit == null)
            {
                return NotFound();
            }

            //if found
            return View(sportToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Sport sport)
        {
            _sportService.UpdateSport(sport);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Select()
        {
            var SportsList = _sportService.GetAllSports();
            return View("Customer/Select", SportsList);
        }
    }
}
