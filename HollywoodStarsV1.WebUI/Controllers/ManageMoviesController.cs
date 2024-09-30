using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HollywoodStars.Data;
using HollywoodStars.Models;

namespace HollywoodStarsV1.WebUI.Controllers
{
    public class ManageMoviesController : CommonBaseClass
    {
        // GET: ManageMovies
        public ActionResult Index()
        {
            var list = new List<Movie>();
            try
            {
                list = MoviesData.GetList(conn);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return View(list);
        }

        // GET: Displays the Add or Update Movie view
        public ActionResult AddOrUpdate(int? id)
        {
            // Early return if no id is provided
            if (!id.HasValue) return View();

            Movie movie = null;

            try
            {
                movie = MoviesData.GetMovie(id.Value, conn);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return View(movie);
        }

        // POST: Add or Update Movie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            try
            {
                if (movie.MovieId == 0)
                {
                    MoviesData.Insert(movie, conn);
                }
                else
                {
                    MoviesData.Update(movie, conn);
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
                return View(movie);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Remove Movie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int movieId)
        {
            try
            {
                var movie = new Movie { MovieId = movieId };
                if (MoviesData.HasActors(movie, conn))
                {
                    throw new Exception("This movie is associated with one or more actors. Remove all the associations first.");
                }
                else
                {
                    MoviesData.Delete(movieId, conn);
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
