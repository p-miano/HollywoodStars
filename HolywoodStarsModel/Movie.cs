using System.ComponentModel.DataAnnotations;

namespace HollywoodStars.Models
{
    public class Movie
    {
        [Display(Name = "Movie Id")]
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }
    }
}
