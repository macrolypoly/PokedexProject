using PokedexProject.Data;
using PokedexProject.Models.Question;
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
                  QuestionId = model.QuestionId,
                  QuestionText = model.QuestionText,
                  ChallengeId = model.ChallengeId,
                  Choices = model.Choices
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
                            Choices = e.Choices
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
