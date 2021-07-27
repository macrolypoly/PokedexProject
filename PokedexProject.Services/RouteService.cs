using PokedexProject.Data;
using PokedexProject.Models;
using PokedexProject.Models.Item;
using PokedexProject.Models.Route;
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
                //var innerJoinQuery =
                // from route in ctx.Routes
                // join item in ctx.Items on route.RouteId equals item.ListOfRoutes.Where(r => r.RouteId == id);
                //var list = 
                //    ctx
                //    .Database.
                //    .Select(i => i.ListOfRoutes.Where(x => x.RouteId == id));

                //var route = ctx.Routes.Find(id);
                //ctx.Items(route)
                //    .Collection()

                var entity =
                    ctx
                    .Routes
                    //.Include(l => l.ListOfItems)
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
                var route =
                    ctx.Routes.SingleOrDefault(e => e.RouteId == model.RouteId);
                var item =
                    ctx.Items.Single(r => r.ItemId == model.ItemId);
                route.ListOfItems.Add(item);

                return ctx.SaveChanges() == 1;
            }
        }
        public bool AddPokemon(RoutePokemon model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var route =
                    ctx.Routes.SingleOrDefault(e => e.RouteId == model.RouteId);
                var pokemon =
                    ctx.Pokemon.Single(r => r.PokemonId == model.PokemonId);
                route.RoutePokemon.Add(pokemon);

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteItem(RouteDeleteItem model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var route =
                    ctx.Routes.SingleOrDefault(e => e.RouteId == model.RouteId);
                var item =
                    ctx.Items.Single(r => r.ItemId == model.ItemId);
                route.ListOfItems.Remove(item);

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeletePokemon(RouteDeletePoke model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var route =
                    ctx.Routes.SingleOrDefault(e => e.RouteId == model.RouteId);
                var poke =
                    ctx.Pokemon.Single(r => r.PokemonId == model.PokemonId);
                route.RoutePokemon.Remove(poke);

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
