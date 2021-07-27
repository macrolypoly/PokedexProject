using PokedexProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models.Route
{
   public class RouteDeletePoke
    {
        public int RouteId { get; set; }
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }

        public ICollection<Pokemon> RoutePokemon { get; set; }
    }
}
