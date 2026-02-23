using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
{
    public class Sport
    {
        public int Id { get; set;}

        [Required(ErrorMessage = "Please provide a sport name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        public string Name { get; set;}

        [Required(ErrorMessage = "Please provide the number or players per team")]
        [Range(1, 100, ErrorMessage = "Players per team must be between 1 and 100 players")]
        public int PlayersPerTeam {get; set;}
    }
}