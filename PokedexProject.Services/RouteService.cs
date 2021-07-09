using PokedexProject.Data;
using PokedexProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                new Route()
                {
                    OwnerId = model.OwnerId,
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
                           OwnerId = e.OwnerId,
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
                       OwnerId = entity.OwnerId,
                       RouteId = entity.RouteId,
                       RouteName = entity.RouteName,
                       ListOfItems = entity.ListOfItems,
                       RoutePokemon = entity.RoutePokemon
                    };
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
                    .Single(e => e.RouteId == pokeId && e.OwnerId == _userId);
                ctx.Routes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
