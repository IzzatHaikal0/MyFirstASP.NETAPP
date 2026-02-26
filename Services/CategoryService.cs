using MyMvcApp.Data;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace MyMvcApp.Services
{
    //execute all the method from interface services
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Category.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Category.Find(id);
        }

        public void CreateCategory(Category category)
        {
            _context.Category.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Category.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            _context.Category.Remove(category);
            _context.SaveChanges();
        }
    }
}