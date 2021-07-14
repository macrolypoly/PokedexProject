using Microsoft.AspNet.Identity;
using PokedexProject.Models;
using PokedexProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokedexProject.Controllers
{
    public class RouteController : Controller
    {
        // GET: Route
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RouteService(userId);
            var model = service.GetRoute();

            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RouteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateRouteService();

            if (service.CreateRoute(model))
            {
                TempData["SaveResult"] = "Your Route was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Route could not be added.");

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateRouteService();
            var detail = service.GetRouteById(id);
            var model =
                new RouteEdit
                {
                    OwnerId = detail.OwnerId,
                    RouteId = detail.RouteId,
                    RouteName = detail.RouteName,
                    ListOfItems = detail.ListOfItems,
                    RoutePokemon = detail.RoutePokemon
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RouteEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.RouteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateRouteService();

            if (service.EditRoute(model))
            {
                TempData["SaveResult"] = "Your Route was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Route could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateRouteService();
            var model = svc.GetRouteById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoute(int id)
        {
            var service = CreateRouteService();

            service.DeleteRoute(id);

            TempData["SaveResult"] = "Route was deleted.";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var svc = CreateRouteService();
            var model = svc.GetRouteById(id);

            return View(model);
        }
        private RouteService CreateRouteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RouteService(userId);
            return service;
        }
    }
}