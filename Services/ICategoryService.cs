using MyMvcApp.Models;
using System.Collections.Generic;

namespace MyMvcApp.Services
{
    //LIST ALL METHOD HERE
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        void CreateCategory(Category category);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);
    }
}