using Microsoft.AspNet.Identity;
using PokedexProject.Data;
using PokedexProject.Models;
using PokedexProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokedexProject.Controllers
{
    public class GameController : Controller
    {
        [HttpGet]
        public ActionResult SelectChallenge()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            ChallengeVM chall = new ChallengeVM();
            chall.ListOfChallenges = (List<Models.SelectListItem>)new ChallengeService(userId).GetChallenge().Select(q => new Models.SelectListItem
            {
                Text = q.ChallengeName,
                Value = q.ChallengeId
            }).ToList();
            ViewBag.ChallengeList = new ChallengeService(userId).GetChallenge();
            return View(chall);
        }
        [HttpPost]
        public ActionResult SelectChallenge(ChallengeVM model)
        {
            ChallengeVM challSelected = new ChallengeService(Guid.Parse(User.Identity.GetUserId())).GetChallenge().Where(q => q.ChallengeId == model.ChallengeId).Select(q => new ChallengeVM
            {
                ChallengeId = q.ChallengeId,
                ChallengeName = q.ChallengeName,
            }).FirstOrDefault();
            
            if(challSelected != null)
            {
                Session["SelectedChallenge"] = challSelected;

                return RedirectToAction("ChallengeQuestion");
            }
            return View();
        }
        [HttpGet]
        public ActionResult ChallengeQuestion()
        {
            ChallengeVM challSelected = Session["SelectedChallenge"] as ChallengeVM;
            IQueryable<QuestionVM> questions = null;

            if(challSelected != null)
            {
                questions = new QuestionService(Guid.Parse(User.Identity.GetUserId())).GetQuestion().Where(q => q.ChallengeId == challSelected.ChallengeId)
                    .Select(q => new QuestionVM
                    {
                        QuestionId = q.QuestionId,
                        QuestionText = q.QuestionText,
                        Choices = q.Choices.Select(c => new Choice
                        {
                            ChoiceId = c.ChoiceId,
                            ChoiceText = c.ChoiceText
                        }).ToList()
                        
                        
                    }).AsQueryable();
                
            }
            return View(questions);
            
        }
        [HttpPost]
        public ActionResult ChallengeQuestion(List<ChallengeAnswer> resultChallenge)
        {
            List<ChallengeAnswer> finalResultChallenge = new List<ChallengeAnswer>();
            foreach(ChallengeAnswer answer in resultChallenge)
            {
                ChallengeAnswer result = (ChallengeAnswer)new AnswerService(Guid.Parse(User.Identity.GetUserId())).GetAnswer().Where(a => a.QuestionId == answer.QuestionId).Select(a => new ChallengeAnswer
                {
                    QuestionId = a.QuestionId,
                    AnswerQ = a.AnswerText,
                    isCorrect = (answer.AnswerQ.ToLower().Equals(a.AnswerText.ToLower()))


                }).FirstOrDefault();

                finalResultChallenge.Add(result);
            }
            return Json(new { result = finalResultChallenge }, JsonRequestBehavior.AllowGet);
        }
    }
}