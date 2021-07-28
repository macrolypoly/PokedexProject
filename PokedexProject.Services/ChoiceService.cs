using PokedexProject.Data;
using PokedexProject.Models.Choice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Services
{
   public class ChoiceService
    {
        private readonly Guid _userId;

        public ChoiceService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateChoice(ChoiceCreate model)
        {
            var entity =
                new Choice()
                {
                    OwnerId = _userId,
                    ChoiceId = model.ChoiceId,
                    ChoiceText = model.ChoiceText,
                    QuestionId = model.QuestionId
                    
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Choices.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<ChoiceListItem> GetChoice()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Choices
                    .Select(
                        e =>
                        new ChoiceListItem
                        {
                            OwnerId = e.OwnerId,
                            ChoiceId = e.ChoiceId,
                            ChoiceText = e.ChoiceText,
                            QuestionId = e.QuestionId
                            
                        }
                        );
                return query.ToArray();
            }
        }
        public ChoiceDetail GetChoiceById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Choices
                    .Single(e => e.ChoiceId == id);
                return
                    new ChoiceDetail
                    {
                        OwnerId = entity.OwnerId,
                        ChoiceId = entity.ChoiceId,
                        ChoiceText = entity.ChoiceText,
                        QuestionId = entity.QuestionId
                        
                    };
            }
        }
        public bool EditChoice(ChoiceEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Choices
                    .Single(e => e.ChoiceId == model.ChoiceId && e.OwnerId == _userId);
                entity.OwnerId = model.OwnerId;
                entity.ChoiceId = model.ChoiceId;
                entity.ChoiceText = model.ChoiceText;
                entity.QuestionId = model.QuestionId;
              
                ;

                return ctx.SaveChanges() == 1;
            }
        }
        //public bool AddAnswer(ChoiceAnswer model)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var Choices =
        //            ctx.Choices.SingleOrDefault(e => e.ChoiceId == model.ChoiceId);
        //        var Answer =
        //            ctx.Answers.SingleOrDefault(r => r.AnswerId == model.AnswerId);
        //        Choices.ListOfAnswers.Add(Answer);

        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        //public bool DeleteAnswers(DeleteAnswer model)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var Choices =
        //            ctx.Choices.SingleOrDefault(e => e.ChoiceId == model.ChoiceId);
        //        var Answers =
        //            ctx.Answers.Single(r => r.AnswerId == model.AnswerId);
        //        Choices.

        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        public bool DeleteChoice(int pokeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Choices
                    .SingleOrDefault(e => e.ChoiceId == pokeId && e.OwnerId == _userId);
                ctx.Choices.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
