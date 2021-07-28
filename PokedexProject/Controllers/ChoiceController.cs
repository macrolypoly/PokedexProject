using Microsoft.AspNet.Identity;
using PokedexProject.Models.Choice;
using PokedexProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokedexProject.Controllers
{
    public class ChoiceController : Controller
    {
        // GET: Choices
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ChoiceService(userId);
            var model = service.GetChoice();

            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.QuestionList = new QuestionService(Guid.Parse(User.Identity.GetUserId())).GetQuestion();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChoiceCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateChoiceService();

            if (service.CreateChoice(model))
            {
                TempData["SaveResult"] = "Your Choice was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Choice could not be added.");

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateChoiceService();
            var detail = service.GetChoiceById(id);
            var model =
                new ChoiceEdit
                {
                    OwnerId = detail.OwnerId,
                    ChoiceId = detail.ChoiceId,
                    ChoiceText = detail.ChoiceText,
                    QuestionId = detail.QuestionId

                };
            return View(model);
        }
        public ActionResult EditChoice(int id, ChoiceEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.ChoiceId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateChoiceService();

            if (service.EditChoice(model))
            {
                TempData["SaveResult"] = "Your choice was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "choice could not be updated.");
            return View(model);
        }
        //public ActionResult AddItem(int Id)
        //{
        //    var service = CreateChoicesService();
        //    var Choices = service.GetChoicesById(Id);
        //    var model =
        //        new ItemListCreate
        //        {
        //            ChoicesId = Id

        //        };
        //    ViewBag.ItemList = new ItemService(Guid.Parse(User.Identity.GetUserId())).GetItem();
        //    return View(model);
        //}
        //[ActionName("DeleteItem")]
        //public ActionResult DeleteItem(int Id)
        //{
        //    var service = CreateChoiceService();
        //    var Choices = service.GetChoicesById(Id);
        //    var model =
        //        new ChoiceDeleteItem
        //        {
        //            ChoicesId = Choices.ChoicesId,
        //            ListOfItems = Choices.ListOfItems
        //        };
        //    return View(model);
        //}
        //[HttpPost]
        //[ActionName("DeleteItem")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteItem(ChoiceDeleteItem model)
        //{
        //    var service = CreateChoicesService();
        //    service.DeleteItem(model);

        //    TempData["SaveResult"] = "Your item was removed.";
        //    return RedirectToAction("Index");
        //}
        //[ActionName("DeletePokemon")]
        //public ActionResult DeletePokemon(int Id)
        //{
        //    var service = CreateChoicesService();
        //    var Choices = service.GetChoicesById(Id);
        //    var model =
        //        new ChoicesDeletePoke
        //        {
        //            ChoicesId = Choices.ChoicesId,
        //            ChoicesPokemon = Choices.ChoicesPokemon
        //        };
        //    return View(model);
        //}
        //[HttpPost]
        //[ActionName("DeletePokemon")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeletePokemon(ChoicesDeletePoke model)
        //{
        //    var service = CreateChoicesService();
        //    service.DeletePokemon(model);

        //    TempData["SaveResult"] = "Your pokemon was removed.";
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddItem(ItemListCreate model)
        //{
        //    var service = CreateChoicesService();

        //    if (service.AddItem(model))
        //    {
        //        TempData["SaveResult"] = "Item was added.";
        //        return RedirectToAction("Index");
        //    }
        //    ModelState.AddModelError("", "Item could not be added.");
        //    ViewBag.ItemList = new ItemService(Guid.Parse(User.Identity.GetUserId())).GetItem();
        //    return View(model);
        //}
        //public ActionResult AddPokemon(int Id)
        //{
        //    var service = CreateChoicesService();
        //    var Choices = service.GetChoicesById(Id);
        //    var model =
        //        new ChoicesPokemon
        //        {
        //            ChoicesId = Id

        //        };
        //    ViewBag.PokemonList = new PokemonService(Guid.Parse(User.Identity.GetUserId())).GetPokemon();
        //    return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddPokemon(ChoicePokemon model)
        //{
        //    var service = CreateChoiceService();

        //    if (service.AddPokemon(model))
        //    {
        //        TempData["SaveResult"] = "Item was added.";
        //        return RedirectToAction("Index");
        //    }
        //    ModelState.AddModelError("", "Item could not be added.");
        //    ViewBag.PokemonList = new PokemonService(Guid.Parse(User.Identity.GetUserId())).GetPokemon();
        //    return View(model);
        //}

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateChoiceService();
            var model = svc.GetChoiceById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteChoice(int id)
        {
            var service = CreateChoiceService();

            service.DeleteChoice(id);

            TempData["SaveResult"] = "Choice was deleted.";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var svc = CreateChoiceService();
            var model = svc.GetChoiceById(id);
            //ViewBag.QuestionList = new ItemService(Guid.Parse(User.Identity.GetUserId())).GetItem();

            return View(model);
        }
        private ChoiceService CreateChoiceService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ChoiceService(userId);
            return service;
        }
    }
}