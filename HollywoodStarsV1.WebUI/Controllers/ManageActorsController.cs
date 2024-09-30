using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HollywoodStars.Data;
using HollywoodStars.Models;

namespace HollywoodStarsV1.WebUI.Controllers
{
    public class ManageActorsController : CommonBaseClass
    {
        // GET: ManageActors
        public ActionResult Index()
        {
            var list = new List<Actor>();
            try
            {
                list = ActorsData.GetList(conn);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return View(list);
        }

        // GET: Displays the Add or Update Actor view
        public ActionResult AddOrUpdate(int? id)
        {
            // Early return if no id is provided
            if (!id.HasValue) return View();

            Actor actor = null;

            try
            {
                actor = ActorsData.GetActor(id.Value, conn);
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return View(actor);
        }

        // POST: Add or Update Actor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            try
            {
                if (actor.ActorId == 0)
                {
                    ActorsData.Insert(actor, conn);
                }
                else
                {
                    ActorsData.Update(actor, conn);
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
                return View(actor);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Remove Actor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int actorId)
        {
            try
            {
                var actor = new Actor { ActorId = actorId };
                if (ActorsData.HasMovies(actor, conn))
                {
                    throw new Exception("This actor is associated with one or more movies. Remove all the associations first.");
                }
                else
                {
                    ActorsData.Delete(actorId, conn);
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