using PokedexProject.Data;
using PokedexProject.Models;
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
                    PokemonId = model.PokemonId,
                    Name = model.Name,
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
                            Name = e.Name,
                            Type = (int)e.Type,
                            Type2 = (int)e.Type2
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
                        Name = entity.Name,
                        Type = (int)entity.Type,
                        Type2 = (int)entity.Type2
                    };
            }
        }
        public bool EditPokemon(PokemonEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Pokemon
                    .Single(e => e.PokemonId == model.PokemonId);
                entity.PokemonId = model.PokemonId;
                entity.Name = model.PokemonName;
                entity.Type = (PokedexProject.Data.PokeType)model.Type;
                entity.Type2 = (PokedexProject.Data.PokeType)model.Type2;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
