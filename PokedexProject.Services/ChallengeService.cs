using PokedexProject.Data;
using PokedexProject.Models.Challenge;
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
                    ChallengeId = model.ChallengeId,
                    ChallengeName = model.ChallengeName,
                    ListOfQuestions = model.ListOfQuestions
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
                            OwnerId = e.OwnerId,
                            ChallengeId = e.ChallengeId,
                            ChallengeName = e.ChallengeName,
                            ListOfQuestions = e.ListOfQuestions
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
        public bool AddQuestion(ChallengeQuestion model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var Challenge =
                    ctx.Challenges.SingleOrDefault(e => e.ChallengeId == model.ChallengeId);
                var question =
                    ctx.Questions.SingleOrDefault(r => r.QuestionId == model.QuestionId);
                Challenge.ListOfQuestions.Add(question);

                return ctx.SaveChanges() == 1;
            }
        }
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
