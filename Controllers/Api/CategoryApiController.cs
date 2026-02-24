using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using System.Linq;

namespace MyMvcApp.Controllers.Api
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryApiController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var CategoryList = _context.Category.ToList();
            return Ok(CategoryList);
        }
    }
}