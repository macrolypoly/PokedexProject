using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Data
{
    public class TrainerPokemon
    {
        [Key, ForeignKey("Pokemon")]
        public int PokemonId { get; set; }
        [ForeignKey("Trainer")]
        public int TrainerId { get; set; }
        public int Count { get; set; }
        public virtual Trainer Trainer { get; set; }
        public virtual Pokemon Pokemon { get; set; }
    }
}
