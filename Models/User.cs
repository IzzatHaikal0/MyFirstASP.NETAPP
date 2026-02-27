using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
{
    public class User
    {
        // The [Key] attribute tells EF Core to make this the Primary Key in the database
        [Key]
        public int Id { get; set; }

        // [Required] tells the database this column cannot be NULL
        [Required(ErrorMessage = "Please provide a valid email")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 50 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
        public string Password { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public string Role { get; set;} = "Admin";

    }
}