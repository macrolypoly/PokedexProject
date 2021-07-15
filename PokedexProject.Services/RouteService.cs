using PokedexProject.Data;
using PokedexProject.Models;
using PokedexProject.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokedexProject.Services
{
    public class RouteService
    {
        private readonly Guid _userId;

        public RouteService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateRoute(RouteCreate model)
        {
            var entity =
                new PokeRoute()
                {
                    OwnerId = _userId,
                    RouteId = model.RouteId,
                    RouteName = model.RouteName,
                    ListOfItems = model.ListOfItems,
                    RoutePokemon = model.RoutePokemon
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Routes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<RouteListItem> GetRoute()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Routes
                    .Select(
                        e =>
                        new RouteListItem
                        {
                           OwnerId = _userId,
                           RouteId = e.RouteId,
                           RouteName = e.RouteName,
                           ListOfItems = e.ListOfItems,
                           RoutePokemon = e.RoutePokemon
                        }
                        );
                return query.ToArray();
            }
        }
        public RouteDetail GetRouteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Routes
                    .Single(e => e.RouteId == id);
                return
                    new RouteDetail
                    {
                       OwnerId = _userId,
                       RouteId = entity.RouteId,
                       RouteName = entity.RouteName,
                       ListOfItems = entity.ListOfItems,
                       RoutePokemon = entity.RoutePokemon
                    };
            }
        }
        public bool AddItem(ItemListCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Routes.SingleOrDefault(e => e.RouteId == model.RouteId);
                var item =
                    ctx.Items.SingleOrDefault(r => r.ItemId == model.ItemId);
                entity.ListOfItems.Add(item);

                return ctx.SaveChanges() == 1;
            }
        }
        public bool EditRoute(RouteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Routes
                    .Single(e => e.RouteId == model.RouteId && e.OwnerId == _userId);
                entity.OwnerId = model.OwnerId;
                entity.RouteId = model.RouteId;
                entity.RouteName = model.RouteName;
                entity.ListOfItems = model.ListOfItems;
                entity.RoutePokemon = model.RoutePokemon;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteRoute(int pokeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Routes
                    .FirstOrDefault(e => e.OwnerId == _userId);
                ctx.Routes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
        
    }
}
