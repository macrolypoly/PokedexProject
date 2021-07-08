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
    [Authorize]
    public class PokemonController : Controller
    {
        // GET: Pokemon
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PokemonService(userId);
            var model = service.GetPokemon();

            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PokemonCreate model)
        {
            if (!ModelState.IsValid) return View(model);
     
            var service = CreatePokemonService();

            if (service.CreatePokemon(model))
            {
                TempData["SaveResult"] = "Your pokemon was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Pokemon could not be added.");

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreatePokemonService();
            var detail = service.GetPokemonById(id);
            var model =
                new PokemonEdit
                {
                    PokemonId = detail.PokemonId,
                    PokemonName = detail.Name,
                    Type = detail.Type,
                    Type2 = detail.Type2
                };
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var svc = CreatePokemonService();
            var model = svc.GetPokemonById(id);

            return View(model);
        }

        private PokemonService CreatePokemonService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PokemonService(userId);
            return service;
        }
    }
}