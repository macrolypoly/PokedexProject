using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokedexProject.Controllers
{
    public class TrainerItemsController : Controller
    {
        // GET: TrainerItems
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TrainerItemsService(userId);
            var model = service.GetTrainerItems();

            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainerItemsCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTrainerItemsService();

            if (service.CreateTrainerItems(model))
            {
                TempData["SaveResult"] = "Your TrainerItems was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "TrainerItems could not be added.");

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateTrainerItemsService();
            var detail = service.GetTrainerItemsById(id);
            var model =
                new TrainerItemsEdit
                {
                    OwnerId = detail.OwnerId,
                    TrainerItemsId = detail.TrainerItemsId,
                    Name = detail.Name,
                    ProfileCreated = detail.ProfileCreated
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TrainerItemsEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.TrainerItemsId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateTrainerItemsService();

            if (service.EditTrainerItems(model))
            {
                TempData["SaveResult"] = "Your TrainerItems was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "TrainerItems could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateTrainerItemsService();
            var model = svc.GetTrainerItemsById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTrainerItems(int id)
        {
            var service = CreateTrainerItemsService();

            service.DeleteTrainerItems(id);

            TempData["SaveResult"] = "TrainerItems was deleted.";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var svc = CreateTrainerItemsService();
            var model = svc.GetTrainerItemsById(id);

            return View(model);
        }


        private TrainerItemsService CreateTrainerItemsService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TrainerItemsService(userId);
            return service;
        }
    }
}