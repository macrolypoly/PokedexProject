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
        //GET: Trainer
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
                TempData["SaveResult"] = "Your trainer was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Trainer could not be created.");

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