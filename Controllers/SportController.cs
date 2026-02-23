using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Data; 
using System.Linq;
using Microsoft.VisualBasic;
using SQLitePCL;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyMvcApp.Namespace
{
    public class SportController : Controller
    {
            
        private readonly AppDbContext _context;

        public SportController(AppDbContext context)
        {
            _context = context;
        }

        // Display list of sports from db
        public IActionResult Index()
        {
            
            var SportsList = _context.Sports.ToList();
            return View(SportsList);
        }

        //show empty form page to add new sports
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        //process the form and store in db
        [HttpPost]
        public IActionResult Create(Sport sport)
        {
            //validate data receive
            if(!ModelState.IsValid)
            {
                //Return the View and send the bad data back so they don't lose what they typed.
                return View(sport);
            }
            _context.Sports.Add(sport);
            _context.SaveChanges();
        
            return RedirectToAction("Index");
        }

        //delete the sport from db (Select based on id)
        public IActionResult Delete(int id)
        {
            var sportToDelete = _context.Sports.Find(id);

            if(sportToDelete != null)
            {
                _context.Sports.Remove(sportToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var sportToEdit = _context.Sports.Find(id);

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
            _context.Sports.Update(sport);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
