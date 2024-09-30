using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using HollywoodStars.Data;
using HollywoodStars.Models;
using HollywoodStars.WebUI.Models;

namespace HollywoodStarsV1.WebUI.Controllers
{
    public class ManageActorMoviesController : CommonBaseClass
    {
        // GET: ManageActorMovies
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                TempData["DangerMessage"] = "Invalid Actor ID.";
                return RedirectToAction("Index", "ManageActors"); // Redirecting if id is null
            }

            Actor actor = null;
            var listOfAssociatedMovies = new List<Movie>();
            var listOfNonAssociatedMovies = new List<Movie>();

            try
            {
                // Using id.Value after checking id.HasValue
                actor = ActorsData.GetActor(id.Value, conn);
                listOfAssociatedMovies = ActorMoviesData.GetAssociatedMovieList(id.Value, conn);
                listOfNonAssociatedMovies = ActorMoviesData.GetNonAssociatedMovieList(id.Value, conn);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
                return RedirectToAction("Index", "ManageActors"); // Redirecting on error
            }

            var myViewModel = new ActorMoviesViewModel
            {
                Actor = actor,
                AssociatedMovies = listOfAssociatedMovies,
                NonAssociatedMovies = listOfNonAssociatedMovies
            };

            return View(myViewModel);
        }

        // POST: Add Actor-Movie association
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int actorId, int movieId)
        {
            try
            {
                ActorMoviesData.Insert(actorId, movieId, conn);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Index), new { id = actorId });
        }

        // POST: Remove Actor-Movie association
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int actorId, int movieId)
        {
            try
            {
                ActorMoviesData.Delete(actorId, movieId, conn);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Index), new { id = actorId });
        }

    }
}
