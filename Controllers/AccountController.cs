using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Data; 
using System.Linq;
using Microsoft.VisualBasic;
using SQLitePCL;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyMvcApp.Controllers
{
    public class AccountController : Controller
    {
        // 3. Create a private variable to hold the database connection
        private readonly AppDbContext _context;

        // 4. THE MAGIC (Dependency Injection): 
        // When ASP.NET Core creates this controller, it automatically passes in the database!
        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            // 5. THE QUERY: Ask the database if a user exists with this email AND password
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user != null)
            {
                // Success! We found a matching user in the database.
                return RedirectToAction("Index", "Home");
            }

            // Failed! The user variable came back as 'null' (not found).
            ViewBag.ErrorMessage = "Invalid email or password.";
            return View(model); 
        }

        // ---------------------------------------------------------
        // TEMPORARY HELPER: We need a way to put a user in our empty database!
        // ---------------------------------------------------------
        [HttpGet]
        public IActionResult CreateTestUser()
        {
            // Check if our database is completely empty
            if (!_context.Users.Any())
            {
                // Create a new User object
                var newUser = new User 
                { 
                    Email = "admin@test.com", 
                    Password = "password123" 
                };

                // Add it to the database and save!
                _context.Users.Add(newUser);
                _context.SaveChanges();

                return Content("Success! Test user created. Go back to /Account/Login to test it.");
            }

            return Content("A user already exists in the database!");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            //translate the ViewModel into a real database model
            var newUser = new User
            {
                Email = model.Email,
                Password = model.Password
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}