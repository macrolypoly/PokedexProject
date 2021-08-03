using Microsoft.AspNet.Identity;
using PokedexProject.Models.Answer;
using PokedexProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokedexProject.Controllers
{
    public class AnswerController : Controller
    {
        // GET: Answers
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AnswerService(userId);
            var model = service.GetAnswer();

            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.QuestionList = new QuestionService(Guid.Parse(User.Identity.GetUserId())).GetQuestion();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnswerCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateAnswerService();

            if (service.CreateAnswer(model))
            {
                TempData["SaveResult"] = "Your Answer was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Answer could not be added.");

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateAnswerService();
            var detail = service.GetAnswerById(id);
            var model =
                new AnswerEdit
                {
                    OwnerId = detail.OwnerId,
                    AnswerId = detail.AnswerId,
                    AnswerText = detail.AnswerText,
                    QuestionId = detail.QuestionId

                };
            ViewBag.QuestionList = new QuestionService(Guid.Parse(User.Identity.GetUserId())).GetQuestion();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AnswerEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.AnswerId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateAnswerService();

            if (service.EditAnswer(model))
            {
                TempData["SaveResult"] = "Your Answer was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Answer could not be updated.");
            return View(model);
        }
        
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateAnswerService();
            var model = svc.GetAnswerById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAnswer(int id)
        {
            var service = CreateAnswerService();

            service.DeleteAnswer(id);

            TempData["SaveResult"] = "Answer was deleted.";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var svc = CreateAnswerService();
            var model = svc.GetAnswerById(id);
            //ViewBag.QuestionList = new ItemService(Guid.Parse(User.Identity.GetUserId())).GetItem();

            return View(model);
        }
        private AnswerService CreateAnswerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AnswerService(userId);
            return service;
        }
    }
}