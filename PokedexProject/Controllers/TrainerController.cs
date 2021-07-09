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
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TrainerService(userId);
            var model = service.GetTrainer();

            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainerCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTrainerService();

            if (service.CreateTrainer(model))
            {
                TempData["SaveResult"] = "Your Trainer was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Trainer could not be added.");

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateTrainerService();
            var detail = service.GetTrainerById(id);
            var model =
                new TrainerEdit
                {
                    OwnerId = detail.OwnerId,
                    TrainerId = detail.TrainerId,
                    TrainerName = detail.TrainerName,
                    ProfileCreated = detail.ProfileCreated
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TrainerEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.TrainerId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateTrainerService();

            if (service.EditTrainer(model))
            {
                TempData["SaveResult"] = "Your Trainer was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Trainer could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateTrainerService();
            var model = svc.GetTrainerById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTrainer(int id)
        {
            var service = CreateTrainerService();

            service.DeleteTrainer(id);

            TempData["SaveResult"] = "Trainer was deleted.";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var svc = CreateTrainerService();
            var model = svc.GetTrainerById(id);

            return View(model);
        }


        private TrainerService CreateTrainerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TrainerService(userId);
            return service;
        }
    }
}