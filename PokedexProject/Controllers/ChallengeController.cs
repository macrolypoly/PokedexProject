using Microsoft.AspNet.Identity;
using PokedexProject.Data;
using PokedexProject.Models.Challenge;
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
    public class ChallengeController : Controller
    {
        // GET: Challenge
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ChallengeService(userId);
            var model = service.GetChallenge();

            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.RouteList = new RouteService(Guid.Parse(User.Identity.GetUserId())).GetRoute();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChallengeCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateChallengeService();

            if (service.CreateChallenge(model))
            {
                TempData["SaveResult"] = "Your Challenge was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Challenge could not be added.");

            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var service = CreateChallengeService();
            var Challenge = service.GetChallengeById(id ?? default);
        
            return View(Challenge);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditChallenge(int id, ChallengeEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.ChallengeId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateChallengeService();

            if (service.EditChallenge(model))
            {
                TempData["SaveResult"] = "Your Challenge was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Challenge could not be updated.");
            return View(model);
        }
        public ActionResult RouteChallenge(int id, RouteChallenge model)
        {
            ChallengeService service = CreateChallengeService();
            service.GetChallengeByRoute(id);

            return View(model);
        }
        //public ActionResult AddQuestion(int Id)
        //{
        //    var service = CreateChallengeService();
        //    var Challenge = service.GetChallengeById(Id);
        //    var model =
        //        new ChallengeQuestion
        //        {
        //       ChallengeId = Id,


        //        };
        //    ViewBag.QuestionList = new QuestionService(Guid.Parse(User.Identity.GetUserId())).GetQuestion();
        //    return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddQuestion(ChallengeQuestion model)
        //{
        //    var service = CreateChallengeService();

        //    if (service.AddQuestion(model))
        //    {
        //        TempData["SaveResult"] = "Question was added.";
        //        return RedirectToAction("Index");
        //    }
        //    ModelState.AddModelError("", "Question could not be added.");
        //    return View(model);
        //}
        [ActionName("DeleteQuestion")]
        public ActionResult DeleteQuestion(int Id)
        {
            var service = CreateChallengeService();
            var challenge = service.GetChallengeById(Id);
            var model =
                new DeleteQuestion
                {
                    ChallengeId = Id,
                    ChallengeName = challenge.ChallengeName,
                    ListOfQuestions = challenge.ListOfQuestions,
                };
            return View(model);
        }
        [HttpPost]
        [ActionName("DeleteQuestion")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestion(DeleteQuestion model)
        {
            var service = CreateChallengeService();
            service.DeleteQuestion(model);

            TempData["SaveResult"] = "Your question was removed.";
            return RedirectToAction("Index");
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateChallengeService();
            var model = svc.GetChallengeById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteChallenge(int id)
        {
            var service = CreateChallengeService();

            service.DeleteChallenge(id);

            TempData["SaveResult"] = "Challenge was deleted.";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var svc = CreateChallengeService();
            var model = svc.GetChallengeById(id);
            ViewBag.QuestionList = new QuestionService(Guid.Parse(User.Identity.GetUserId())).GetQuestion();

            return View(model);
        }


        private ChallengeService CreateChallengeService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ChallengeService(userId);
            return service;
        }
    }
}