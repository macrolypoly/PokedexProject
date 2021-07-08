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
                    Name = model.Name,
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
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new TrainerListItem
                        {
                            OwnerId = e.OwnerId,
                            TrainerId = e.TrainerId,
                            Name = e.Name,
                            ProfileCreated = e.ProfileCreated
                        }
                        );
                return query.ToArray();
            }
        }
    }
}
