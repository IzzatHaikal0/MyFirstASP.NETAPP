using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
{
    public class User
    {
        // The [Key] attribute tells EF Core to make this the Primary Key in the database
        [Key]
        public int Id { get; set; }

        // [Required] tells the database this column cannot be NULL
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}