using Microsoft.AspNet.Identity;
using PokedexProject.Models;
using PokedexProject.Models.Route;
using PokedexProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PokedexProject.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ItemService(userId);
            var model = service.GetItem();

            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateItemService();

            if (service.CreateItem(model))
            {
                TempData["SaveResult"] = "Your Item was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Item could not be added.");

            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var service = CreateItemService();
            var item = service.GetItemById(id ?? default );
            //ViewBag.RouteList = new RouteService(Guid.Parse(User.Identity.GetUserId())).GetRoute();
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(int id, ItemEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.ItemId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateItemService();

            if (service.EditItem(model))
            {
                TempData["SaveResult"] = "Your Item was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Item could not be updated.");
            return View(model);
        }
        public ActionResult AddRouteToItem(int Id)
        {
            ViewBag.ID = Id;
            ViewBag.RouteList = new RouteService(Guid.Parse(User.Identity.GetUserId())).GetRoute();
            return View();
        }
        public ActionResult EditRoutes(int id, RouteListCreate model)
        {
           
            if (!ModelState.IsValid) return View(model);
            if (model.ItemId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateItemService();

            if (service.AddRoute(model))
            {
                TempData["SaveResult"] = "Route was added.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "route could not be added.");
            return View(model);
        }
       
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateItemService();
            var model = svc.GetItemById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(int id)
        {
            var service = CreateItemService();

            service.DeleteItem(id);

            TempData["SaveResult"] = "Item was deleted.";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var svc = CreateItemService();
            var model = svc.GetItemById(id);
            ViewBag.RouteList = new RouteService(Guid.Parse(User.Identity.GetUserId())).GetRoute();

            return View(model);
        }


        private ItemService CreateItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ItemService(userId);
            return service;
        }
    }
}