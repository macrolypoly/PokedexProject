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
    public class PokemonService
    {
        private readonly Guid _userId;

        public PokemonService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreatePokemon(PokemonCreate model)
        {
            var entity =
                new Pokemon()
                {
                    OwnerId = _userId,
                    PokemonId = model.PokemonId,
                    PokemonName = model.PokemonName,
                    Type = (PokedexProject.Data.PokeType)model.Type,
                    Type2 = (PokedexProject.Data.PokeType)model.Type2
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Pokemon.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<PokemonListItem> GetPokemon()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Pokemon
                    .Select(
                        e =>
                        new PokemonListItem
                        {
                            PokemonId = e.PokemonId,
                            PokemonName = e.PokemonName,
                            Type = (PokedexProject.Models.PokeType)e.Type,
                            Type2 = (PokedexProject.Models.PokeType)e.Type2,
                            RoutesFound = e.RoutesFound
                        }
                        );
                return query.ToArray();
            }
        }
        public PokemonDetail GetPokemonById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Pokemon
                    .Single(e => e.PokemonId == id);
                return
                    new PokemonDetail
                    {
                        PokemonId = entity.PokemonId,
                        PokemonName = entity.PokemonName,
                        Type = (Models.PokeType)entity.Type,
                        Type2 = (Models.PokeType)entity.Type2,
                        RoutesFound = entity.RoutesFound
                    };
            }
        }
        public bool AddRoute(RoutePokemon model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var poke =
                    ctx.Pokemon.SingleOrDefault(e => e.PokemonId == model.PokemonId);
                var route =
                    ctx.Routes.SingleOrDefault(r => r.RouteId == model.RouteId);
                poke.RoutesFound.Add(route);

                return ctx.SaveChanges() == 1;
            }
        }
        public bool EditPokemon(PokemonEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Pokemon
                    .Single(e => e.PokemonId == model.PokemonId && e.OwnerId == _userId);
                entity.PokemonId = model.PokemonId;
                entity.PokemonName = model.PokemonName;
                entity.Type = (PokedexProject.Data.PokeType)model.Type;
                entity.Type2 = (PokedexProject.Data.PokeType)model.Type2;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeletePokemon(int pokeId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Pokemon
                    .Single(e => e.PokemonId == pokeId && e.OwnerId == _userId);
                ctx.Pokemon.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
