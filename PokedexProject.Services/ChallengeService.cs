using PokedexProject.Data;
using PokedexProject.Models;
using PokedexProject.Models.Challenge;
using PokedexProject.Models.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Services
{
    public class ChallengeService
    {
        private readonly Guid _userId;

        public ChallengeService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateChallenge(ChallengeCreate model)
        {
            var entity =
                new Challenge()
                {
                    OwnerId = _userId,
                    ChallengeName = model.ChallengeName,
                    RouteId = model.RouteId
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Challenges.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<ChallengeListItem> GetChallenge()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Challenges
                    .Select(
                        e =>
                        new ChallengeListItem
                        {
                            ChallengeId = e.ChallengeId,
                            ChallengeName = e.ChallengeName,
                        }
                        );
                return query.ToArray();
            }
        }
        public ChallengeDetail GetChallengeById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Challenges
                    .Single(e => e.ChallengeId == id);
                return
                    new ChallengeDetail
                    {
                        OwnerId = entity.OwnerId,
                        ChallengeId = entity.ChallengeId,
                        ChallengeName = entity.ChallengeName,
                        ListOfQuestions = entity.ListOfQuestions
                    };
            }
        }
        public IEnumerable<RouteChallenge> GetChallengeByRoute(int id)
        {
            List<Question> questions = new QuestionService(_userId).GetQuestionByRoute(id);
            //List<List<Choice>> choice = GetChoiesByQuestions(questions);
            using (var ctx = new ApplicationDbContext())
            {
                
                var entity =
                    ctx
                    .Challenges
                    .Where(e => e.RouteId == id)
                    .Select(
                        e =>
                new RouteChallenge
                                 {
                                     ChallengeId = e.ChallengeId,
                                     ChallengeName = e.ChallengeName,
                                     ListOfQuestions = e.ListOfQuestions,
                                     Choices = e.Choices
                                 });
                //foreach(var rc in entity)
                //{
                //    rc.Choices = GetChoiesByQuestions(questions);
                //}
                return entity.ToList();
            }
        }
        public RouteChallenge GetRouteChallenge(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Challenges
                    .Select(
                        e =>
                new RouteChallenge
                {
                    ChallengeId = e.ChallengeId,
                    ChallengeName = e.ChallengeName,
                    ListOfQuestions = e.ListOfQuestions,
                    Choices = e.Choices
                })
                    .Where(e => e.ChallengeId == id);
                return entity.Single();
            }
        }

        public List<List<Choice>> GetChoiesByQuestions(List<Question> listOfQuestions)
        {
            List<List<Choice>> listOfChoices = new List<List<Choice>>();
            foreach (Question question in listOfQuestions)
            {
                List<Choice> list = new List<Choice>();
                foreach (Choice choice in question.Choices)
                {
                    list.Add(choice);
                }
                    listOfChoices.Add(list);
            }

            //foreach(List<Choice> choiceList in listOfChoices)
            //{
            //    foreach(Choice x in choiceList )
            //    {
            //        foreach(Question question in listOfQuestions)
            //        {
            //            if(x.QuestionId == question.QuestionId)
            //            {

            //            }
            //        }
            //    }
            //}

            return listOfChoices;
        }
       public bool CheckChoice(RouteChallenge model)
        {
            using (var ctx = new ApplicationDbContext())
            {

                //foreach (var item in model)
                //{
                    foreach (var userSelected in model.UserSelected)
                    {
                        var entity =
                            ctx
                            .Answers.Single(e => e.QuestionId == userSelected.QuestionId);
                        for (int i = 0; i < model.Choices.Count; i++)
                        {
                            if (entity.AnswerText == userSelected.UserAnswer)
                            {
                                i++;
                            }
                            decimal percentage = i / model.ListOfQuestions.Count();
                            if (percentage < 50)
                            {
                                return false;
                            }
                            else if (percentage > 40)
                                return true;
                            //return if pass or not

                        }
                    }
                //}
                return false;

            }
        }
        public bool EditChallenge(ChallengeEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Challenges
                    .Single(e => e.ChallengeId == model.ChallengeId && e.OwnerId == _userId);
                entity.OwnerId = model.OwnerId;
                entity.ChallengeId = model.ChallengeId;
                entity.ChallengeName = model.ChallengeName;
                entity.ListOfQuestions = model.ListOfQuestions;

                return ctx.SaveChanges() == 1;
            }
        }
        //public bool AddQuestion(ChallengeQuestion model)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var Challenge =
        //            ctx.Challenges.SingleOrDefault(e => e.ChallengeId == model.ChallengeId);
        //        var question =
        //            ctx.Questions.SingleOrDefault(r => r.QuestionId == model.QuestionId);
        //        Challenge.ListOfQuestions.Add(question);

        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        public bool DeleteQuestion(DeleteQuestion model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var challenge =
                    ctx.Challenges.SingleOrDefault(e => e.ChallengeId == model.ChallengeId);
                var question =
                    ctx.Questions.Single(r => r.QuestionId == model.QuestionId);
                challenge.ListOfQuestions.Remove(question);

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteChallenge(int pokeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Challenges
                    .SingleOrDefault(e => e.ChallengeId == pokeId && e.OwnerId == _userId);
                ctx.Challenges.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    
}
}
