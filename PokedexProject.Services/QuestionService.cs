using PokedexProject.Data;
using PokedexProject.Models;
using PokedexProject.Models.Question;
using PokedexProject.Models.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Services
{
    public class QuestionService
    {
        private readonly Guid _userId;

        public QuestionService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateQuestion(QuestionCreate model)
        {

            var entity =
                new Question()
                {
                    OwnerId = _userId,
                    QuestionText = model.QuestionText,
                    ChallengeId = model.ChallengeId,
                    AnswerId = model.AnswerId
                };
            using (var ctx = new ApplicationDbContext())
            {
                var challenge =
                    ctx.Challenges.SingleOrDefault(e => e.ChallengeId == entity.ChallengeId);
                challenge.ListOfQuestions.Add(entity);
                ctx.Questions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<QuestionListItem> GetQuestion()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Questions
                    .Select(
                        e =>
                        new QuestionListItem
                        {
                            OwnerId = e.OwnerId,
                            QuestionId = e.QuestionId,
                            QuestionText = e.QuestionText,
                            ChallengeId = e.ChallengeId,
                            Choices = e.Choices,
                        }
                        );
                return query.ToArray();
            }
        }
        public QuestionDetail GetQuestionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Questions
                    .Single(e => e.QuestionId == id);
                return
                    new QuestionDetail
                    {
                        OwnerId = entity.OwnerId,
                        QuestionId = entity.QuestionId,
                        QuestionText = entity.QuestionText,
                        ChallengeId = entity.ChallengeId,
                        Choices = entity.Choices
                    };
            }
        }
        public List<Question> GetQuestionByRoute(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                return (from p in ctx.Questions
                        where p.Challenge.RouteId == id
                        select new
                        {
                            OwnerId = p.OwnerId,
                            QuestionId = p.QuestionId,
                            QuestionText = p.QuestionText,
                            ChallengeId = p.ChallengeId,
                            Challenge = p.Challenge,
                            Choices = p.Choices
                        }).ToList()
                        .Select(x => new Question
                        {
                            OwnerId = x.OwnerId,
                            QuestionId = x.QuestionId,
                            QuestionText = x.QuestionText,
                            ChallengeId = x.ChallengeId,
                            Challenge = x.Challenge,
                            Choices = x.Choices
                        }).ToList();
                //var entity =
                //    ctx
                //    .Questions
                //    .Where(e => e.Challenge.Route.RouteId == id)
                //    .Select(e => new Question()
                //    {
                //        OwnerId = e.OwnerId,
                //        QuestionId = e.QuestionId,
                //        QuestionText = e.QuestionText,
                //        ChallengeId = e.ChallengeId,
                //        Choices = e.Choices
                //    }).ToList();
                //return entity;
            }
        }
        public bool EditQuestion(QuestionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Questions
                    .Single(e => e.QuestionId == model.QuestionId && e.OwnerId == _userId);
                entity.OwnerId = model.OwnerId;
                entity.QuestionId = model.QuestionId;
                entity.QuestionText = model.QuestionText;
                entity.ChallengeId = model.ChallengeId;
                entity.Choices = model.Choices;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool AddChoices(QuestionChoices model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var question =
                    ctx.Questions.SingleOrDefault(e => e.QuestionId == model.QuestionId);
                var choice =
                    ctx.Choices.Single(r => r.ChoiceId == model.ChoiceId);
                question.Choices.Add(choice);
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteChoices(DeleteChoices model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var question =
                      ctx.Questions.SingleOrDefault(e => e.QuestionId == model.QuestionId);
                var choice =
                    ctx.Choices.Single(r => r.ChoiceId == model.ChoiceId);
                question.Choices.Remove(choice);

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteQuestion(int pokeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Questions
                    .SingleOrDefault(e => e.QuestionId == pokeId && e.OwnerId == _userId);
                ctx.Questions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
