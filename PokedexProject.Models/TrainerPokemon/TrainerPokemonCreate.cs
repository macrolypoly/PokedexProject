using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models.TrainerPokemon
{
    public class TrainerPokemonCreate
    {
        [Key]
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }
        public int TrainerId { get; set; }
        public int Count { get; set; }
    }
}
