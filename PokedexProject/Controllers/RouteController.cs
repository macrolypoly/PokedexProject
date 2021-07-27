using Microsoft.AspNet.Identity;
using PokedexProject.Models;
using PokedexProject.Models.Item;
using PokedexProject.Models.Route;
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
        public ActionResult EditRoute(int id, RouteEdit model)
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
                TempData["SaveResult"] = "Your Item was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Item could not be updated.");
            return View(model);
        }
        public ActionResult AddItem(int Id)
        {
            var service = CreateRouteService();
            var route = service.GetRouteById(Id);
            var model =
                new ItemListCreate
                {
                    RouteId = Id
                    
                };
            ViewBag.ItemList = new ItemService(Guid.Parse(User.Identity.GetUserId())).GetItem();
            return View(model);
        }
        [ActionName("DeleteItem")]
        public ActionResult DeleteItem(int Id)
        {
            var service = CreateRouteService();
            var route = service.GetRouteById(Id);
            var model =
                new RouteDeleteItem
                {
                    RouteId = route.RouteId,
                    ListOfItems = route.ListOfItems
                };
            return View(model);
        }
        [HttpPost]
        [ActionName("DeleteItem")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(RouteDeleteItem model)
        {
            var service = CreateRouteService();
            service.DeleteItem(model);

            TempData["SaveResult"] = "Your item was removed.";
            return RedirectToAction("Index");
        }
        [ActionName("DeletePokemon")]
        public ActionResult DeletePokemon(int Id)
        {
            var service = CreateRouteService();
            var route = service.GetRouteById(Id);
            var model =
                new RouteDeletePoke
                {
                    RouteId = route.RouteId,
                    RoutePokemon = route.RoutePokemon
                };
            return View(model);
        }
        [HttpPost]
        [ActionName("DeletePokemon")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePokemon(RouteDeletePoke model)
        {
            var service = CreateRouteService();
            service.DeletePokemon(model);

            TempData["SaveResult"] = "Your pokemon was removed.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItem(ItemListCreate model)
        {
            var service = CreateRouteService();

            if(service.AddItem(model))
            {
                TempData["SaveResult"] = "Item was added.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Item could not be added.");
            ViewBag.ItemList = new ItemService(Guid.Parse(User.Identity.GetUserId())).GetItem();
            return View(model);
        }
        public ActionResult AddPokemon(int Id)
        {
            var service = CreateRouteService();
            var route = service.GetRouteById(Id);
            var model =
                new RoutePokemon
                {
                    RouteId = Id

                };
            ViewBag.PokemonList = new PokemonService(Guid.Parse(User.Identity.GetUserId())).GetPokemon();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPokemon(RoutePokemon model)
        {
            var service = CreateRouteService();

            if (service.AddPokemon(model))
            {
                TempData["SaveResult"] = "Item was added.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Item could not be added.");
            ViewBag.PokemonList = new PokemonService(Guid.Parse(User.Identity.GetUserId())).GetPokemon();
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
            ViewBag.ItemList = new ItemService(Guid.Parse(User.Identity.GetUserId())).GetItem();

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