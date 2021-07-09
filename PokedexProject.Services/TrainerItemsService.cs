using PokedexProject.Data;
using PokedexProject.Models;
using PokedexProject.Models.TrainerItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Services
{
    public class TrainerItemsService
    {
        private readonly Guid _userId;

        public TrainerItemsService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateTrainerItems(TrainerItemsCreate model)
        {
            var entity =
                new TrainerItems()
                {
                   ItemId = model.ItemId,
                   TrainerId = model.TrainerId,
                   TrainerName = model.TrainerName,
                   Count = model.Count
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.TrainerItems.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<TrainerItemsListItem> GetTrainerItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .TrainerItems
                    .Select(
                        e =>
                        new TrainerItemsListItem
                        {
                            ItemId = e.ItemId,
                            TrainerId = e.TrainerId,
                            TrainerName = e.TrainerName,
                            Count = e.Count
                        }
                        );
                return query.ToArray();
            }
        }
        public TrainerItemsDetail GetTrainerItemsById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .TrainerItems
                    .Single(e => e.ItemId == id);
                return
                    new TrainerItemsDetail
                    {
                        ItemId = entity.ItemId,
                        TrainerId = entity.TrainerId,
                        TrainerName = entity.TrainerName,
                        Count = entity.Count
                    };
            }
        }
        public bool EditTrainerItems(TrainerItemsEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .TrainerItems
                    .Single(e => e.ItemId == model.ItemId);
                entity.ItemId = model.ItemId;
                entity.TrainerId = model.TrainerId;
                entity.TrainerName = model.TrainerName;
                entity.Count = model.Count;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteTrainerItems(int itemId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .TrainerItems
                    .Single(e => e.ItemId == itemId);
                ctx.TrainerItems.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
