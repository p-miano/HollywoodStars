using HollywoodStars.Models;
using System.Collections.Generic;

namespace HollywoodStars.WebUI.Models
{
    public class ActorMoviesViewModel
    {
        public Actor Actor { get; set; }
        public List<Movie> AssociatedMovies { get; set; }
        public List<Movie> NonAssociatedMovies { get; set; }
    }
}