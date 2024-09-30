using System.ComponentModel.DataAnnotations;

namespace HollywoodStars.Models
{
    public class Actor
    {
        [Display(Name = "Actor Id")]
        public int ActorId { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;
    }
}
