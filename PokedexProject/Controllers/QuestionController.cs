using Microsoft.AspNet.Identity;
using PokedexProject.Models.Question;
using PokedexProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PokedexProject.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new QuestionService(userId);
            var model = service.GetQuestion();

            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.ChallengeList = new ChallengeService(Guid.Parse(User.Identity.GetUserId())).GetChallenge();
            ViewBag.AnswerList = new AnswerService(Guid.Parse(User.Identity.GetUserId())).GetAnswer();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateQuestionService();

            if (service.CreateQuestion(model))
            {
                TempData["SaveResult"] = "Your Question was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Question could not be added.");

            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var service = CreateQuestionService();
            var Question = service.GetQuestionById(id ?? default);

            return View(Question);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditQuestion(int id, QuestionEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.QuestionId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateQuestionService();

            if (service.EditQuestion(model))
            {
                TempData["SaveResult"] = "Your Question was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Question could not be updated.");
            return View(model);
        }
        public ActionResult EditChallenge(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var service = CreateQuestionService();
            var Question = service.GetQuestionById(id ?? default);

            return View(Question);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditChallenge(int id, QuestionEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.QuestionId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateQuestionService();

            if (service.EditQuestion(model))
            {
                TempData["SaveResult"] = "Your Question was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Question could not be updated.");
            return View(model);
        }
        public ActionResult AddChoices(int Id)
        {
            var service = CreateQuestionService();
            var Question = service.GetQuestionById(Id);
            var model =
                new QuestionChoices
                {
                    QuestionId = Id,


                };
            //ViewBag.ChoicesList = new ChoicesService(Guid.Parse(User.Identity.GetUserId())).GetChoices();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddChoices(QuestionChoices model)
        {
            var service = CreateQuestionService();

            if (service.AddChoices(model))
            {
                TempData["SaveResult"] = "Choice was added.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Choice could not be added.");
            return View(model);
        }
        [ActionName("DeleteChoices")]
        public ActionResult DeleteChoices(int Id)
        {
            var service = CreateQuestionService();
            var Question = service.GetQuestionById(Id);
            var model =
                new DeleteChoices
                {
                    QuestionId = Id,
                    QuestionText = Question.QuestionText,
                    Choices = Question.Choices
                };
            return View(model);
        }
        [HttpPost]
        [ActionName("DeleteQuestion")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestion(DeleteChoices model)
        {
            var service = CreateQuestionService();
            service.DeleteChoices(model);

            TempData["SaveResult"] = "Your choice was removed.";
            return RedirectToAction("Index");
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateQuestionService();
            var model = svc.GetQuestionById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestion(int id)
        {
            var service = CreateQuestionService();

            service.DeleteQuestion(id);

            TempData["SaveResult"] = "Question was deleted.";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var svc = CreateQuestionService();
            var model = svc.GetQuestionById(id);
            ViewBag.ChoiceList = new ChoiceService(Guid.Parse(User.Identity.GetUserId())).GetChoice();

            return View(model);
        }


        private QuestionService CreateQuestionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new QuestionService(userId);
            return service;
        }
    }
}