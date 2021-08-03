using PokedexProject.Data;
using PokedexProject.Models;
using PokedexProject.Models.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Services
{
   public class ItemService
    {
        private readonly Guid _userId;

        public ItemService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateItem(ItemCreate model)
        {
            var entity =
                new PokeItem()
                {
                   OwnerId = _userId,
                   ItemId = model.ItemId,
                   ItemName = model.ItemName,
                   Description = model.Description
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Items.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<ItemListItem> GetItem()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Items
                    .Select(
                        e =>
                        new ItemListItem
                        {
                            OwnerId = e.OwnerId,
                            ItemId = e.ItemId,
                            ItemName = e.ItemName,
                            Description = e.Description,
                            ListOfRoutes = e.ListOfRoutes
                        }
                        );
                return query.ToArray();
            }
        }
        public ItemDetail GetItemById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Items
                    .Single(e => e.ItemId == id);
                return
                    new ItemDetail
                    {
                        OwnerId = entity.OwnerId,
                        ItemId = entity.ItemId,
                        ItemName = entity.ItemName,
                        Description = entity.Description,
                        ListOfRoutes = entity.ListOfRoutes
                    };
            }
        }
        public bool EditItem(ItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Items
                    .Single(e => e.ItemId == model.ItemId && e.OwnerId == _userId);
                entity.OwnerId = model.OwnerId;
                entity.ItemId = model.ItemId;
                entity.ItemName = model.ItemName;
                entity.Description = model.Description;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool AddRoute(RouteListCreate model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var item =
                    ctx.Items.SingleOrDefault(e => e.ItemId == model.ItemId);
                var route =
                    ctx.Routes.SingleOrDefault(r => r.RouteId == model.RouteId);
                item.ListOfRoutes.Add(route);

                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<PokeItem> GetItemByRoute(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                ctx
                .Items
                .Where(e => e.ListOfRoutes.Single(x => x.RouteId == id).RouteId == id)
                .Select(
                    z =>
                    new PokeItem
                    {
                        OwnerId = z.OwnerId,
                        ItemId = z.ItemId,
                        ItemName = z.ItemName,
                        ListOfRoutes = z.ListOfRoutes,
                        Description = z.Description
                    });
                return entity.ToList();
            }
        }
        public PokeItem PickRandomItem(IEnumerable<PokeItem> model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var random = new Random();
                var item = model.ToList().OrderBy(s => random.NextDouble()).First();
                var entity =
                    ctx
                    .Items
                    .Single(e => e.ItemId == item.ItemId);
                return entity;
            }
        }
        public bool DeleteItem(int pokeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Items
                    .SingleOrDefault(e => e.ItemId == pokeId && e.OwnerId == _userId);
                ctx.Items.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
