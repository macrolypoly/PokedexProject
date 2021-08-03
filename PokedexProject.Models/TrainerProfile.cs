using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models
{
    public class TrainerProfile
    {
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }
        public int TrainerId { get; set; }
        public int PokeCount { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int ItemCount { get; set; }
    }
}
