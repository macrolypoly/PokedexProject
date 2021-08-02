using PokedexProject.Data;
using PokedexProject.Models.Answer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokedexProject.Services
{
    public class AnswerService
    {
        private readonly Guid _userId;

        public AnswerService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateAnswer(AnswerCreate model)
        {
            var entity =
                new Answer()
                {
                    OwnerId = _userId,
                    AnswerText = model.AnswerText,
                    QuestionId = 0
                    
                };
            //var choice =
            //    new Choice()
            //    {
            //        OwnerId = _userId,
            //        ChoiceText = entity.AnswerText,
            //        QuestionId = entity.QuestionId
            //    };
            using (var ctx = new ApplicationDbContext())
            {
                //ctx.Choices.Add(choice);
                ctx.Answers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<AnswerListItem> GetAnswer()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Answers
                    .Select(
                        e =>
                        new AnswerListItem
                        {
                            OwnerId = e.OwnerId,
                            AnswerId = e.AnswerId,
                            AnswerText = e.AnswerText,
                            QuestionId = e.QuestionId
                            
                        }
                        );
                return query.ToArray();
            }
        }
        public AnswerDetail GetAnswerById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Answers
                    .Single(e => e.AnswerId == id);
                return
                    new AnswerDetail
                    {
                        OwnerId = entity.OwnerId,
                        AnswerId = entity.AnswerId,
                        AnswerText = entity.AnswerText,
                        QuestionId = entity.QuestionId
                        
                    };
            }
        }
        public bool EditAnswer(AnswerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Answers
                    .Single(e => e.AnswerId == model.AnswerId && e.OwnerId == _userId);
                entity.OwnerId = model.OwnerId;
                entity.AnswerId = model.AnswerId;
                entity.AnswerText = model.AnswerText;
                entity.QuestionId = model.QuestionId;
              
                ;

                return ctx.SaveChanges() == 1;
            }
        }
       public Answer GetAnswerByQuestion(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Answers
                    .Single(e => e.QuestionId == id);
                return new Answer
                {
                    OwnerId = entity.OwnerId,
                    AnswerId = entity.AnswerId,
                    AnswerText = entity.AnswerText,
                    QuestionId = entity.QuestionId
                };
            }
        }
        public bool DeleteAnswer(int pokeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Answers
                    .SingleOrDefault(e => e.AnswerId == pokeId && e.OwnerId == _userId);
                ctx.Answers.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
