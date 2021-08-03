using PokedexProject.Data;
using PokedexProject.Models;
using PokedexProject.Models.TrainerItems;
using PokedexProject.Models.TrainerPokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Services
{
    public class TrainerPokemonService
    {
        private readonly Guid _userId;

        public TrainerPokemonService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateTrainerPokemon(TrainerPokemonCreate model)
        {
            var entity =
                new TrainerPokemon()
                {
                    PokemonId = model.PokemonId,
                    PokemonName = model.PokemonName,
                    TrainerId = model.TrainerId,
                    Count = model.Count
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.TrainerPokemon.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<TrainerPokemonListItem> GetTrainerPokemon()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .TrainerPokemon
                    .Select(
                        e =>
                        new TrainerPokemonListItem
                        {
                            PokemonId = e.PokemonId,
                            PokemonName = e.PokemonName,
                            TrainerId = e.TrainerId,
                            TrainerName = e.Trainer.TrainerName,
                            Count = e.Count
                        }
                        );
                return query.ToArray();
            }
        }
        public TrainerPokemonDetail GetTrainerPokemonById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .TrainerPokemon
                    .Single(e => e.PokemonId == id);
                return
                    new TrainerPokemonDetail
                    {
                        PokemonId = entity.PokemonId,
                        PokemonName = entity.PokemonName,
                        TrainerId = entity.TrainerId,
                        TrainerName = entity.Trainer.TrainerName,
                        Count = entity.Count
                    };
            }
        }
        public bool EditTrainerPokemon(TrainerPokemonEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .TrainerPokemon
                    .Single(e => e.PokemonId == model.PokemonId);
                entity.Count = model.Count;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteTrainerPokemon(int PokemonId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .TrainerPokemon
                    .Single(e => e.PokemonId == PokemonId);
                ctx.TrainerPokemon.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }

}
