using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
{
    public class Category
    {
        // Make sure this says CategoryId and not just Id!
        public int CategoryId { get; set; } 
        
        [Required(ErrorMessage = "Please profide a category name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name Must be between 2 and 50 words")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please profide a description")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Description Must be between 2 and 50 words")]
        public string Description { get; set; } = string.Empty;
    }
}