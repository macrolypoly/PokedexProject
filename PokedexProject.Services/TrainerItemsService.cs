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
                    OwnerId = _userId,
                    TrainerItemsId = model.TrainerItemsId,
                    Name = model.Name,
                    Type = (PokedexProject.Data.PokeType)model.Type,
                    Type2 = (PokedexProject.Data.PokeType)model.Type2
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
                            TrainerItemsId = e.TrainerItemsId,
                            Name = e.Name,
                            Type = (int)e.Type,
                            Type2 = (int)e.Type2
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
                    .Single(e => e.TrainerItemsId == id);
                return
                    new TrainerItemsDetail
                    {
                        TrainerItemsId = entity.TrainerItemsId,
                        Name = entity.Name,
                        Type = (int)entity.Type,
                        Type2 = (int)entity.Type2
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
                    .Single(e => e.TrainerItemsId == model.TrainerItemsId && e.OwnerId == _userId);
                entity.TrainerItemsId = model.TrainerItemsId;
                entity.Name = model.TrainerItemsName;
                entity.Type = (PokedexProject.Data.PokeType)model.Type;
                entity.Type2 = (PokedexProject.Data.PokeType)model.Type2;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteTrainerItems(int pokeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .TrainerItems
                    .Single(e => e.TrainerItemsId == pokeId && e.OwnerId == _userId);
                ctx.TrainerItems.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
