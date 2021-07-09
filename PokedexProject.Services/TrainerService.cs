using PokedexProject.Data;
using PokedexProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Services
{
    public class TrainerService
    {
        private readonly Guid _userId;

        public TrainerService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateTrainer(TrainerCreate model)
        {
            var entity =
                new Trainer()
                {
                    OwnerId = _userId,
                    TrainerId = model.TrainerId,
                    TrainerName = model.TrainerName,
                    ProfileCreated = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Trainers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<TrainerListItem> GetTrainer()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Trainers
                    .Select(
                        e =>
                        new TrainerListItem
                        {
                            OwnerId = _userId,
                            TrainerId = e.TrainerId,
                            TrainerName = e.TrainerName,
                            ProfileCreated = e.ProfileCreated
                        }
                        );
                return query.ToArray();
            }
        }
        public TrainerDetail GetTrainerById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Trainers
                    .Single(e => e.TrainerId == id);
                return
                    new TrainerDetail
                    {
                        OwnerId = entity.OwnerId,
                        TrainerId = entity.TrainerId,
                        TrainerName = entity.TrainerName,
                        ProfileCreated = entity.ProfileCreated
                    };
            }
        }
        public bool EditTrainer(TrainerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Trainers
                    .Single(e => e.TrainerId == model.TrainerId);
                entity.OwnerId = model.OwnerId;
                entity.TrainerId = model.TrainerId;
                entity.TrainerName = model.TrainerName;
                entity.ProfileCreated = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteTrainer(int pokeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Trainers
                    .Single(e => e.TrainerId == pokeId);
                ctx.Trainers.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
