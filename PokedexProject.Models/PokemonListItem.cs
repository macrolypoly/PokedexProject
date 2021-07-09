using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models
{
    public class PokemonListItem
    {
        public Guid OwnerId { get; set; }
        public int PokemonId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Type2 { get; set; }

    }
    //public enum Type
    //{
    //    Normal,
    //    Fire,
    //    Water,
    //    Grass,
    //    Electric,
    //    Ice,
    //    Fighting,
    //    Poison,
    //    Ground,
    //    Flying,
    //    Psychic,
    //    Bug,
    //    Rock,
    //    Ghost,
    //    Dark,
    //    Dragon,
    //    Steel,
    //    Fairy
    //}
}
