using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Data; 
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace MyMvcApp.Controllers
{
    public class AccountController : Controller
    {
        // database connection
        private readonly AppDbContext _context;

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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password && u.Role == model.Role);

            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            // 2. The user is real! Let's write their info on the "Wristband" (Claims)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // 3. Package the wristband up (Identity & Principal)
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // 4. THE MAGIC: Hand the secure Cookie to their browser!
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            // Send them to the Home page after a successful login
            if(user.Role == "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("CustomerHome", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
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
                Password = model.Password,
                Role = model.Role
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}