using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Data
{
    public class Pokemon
    {
        [Key]
        public int PokemonId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Pokemon Type")]
        public Type Type { get; set; }
        [Required]
        [Display(Name = "Pokemon Caught")]
        public int NumCaught { get; set; }

    }
   public enum Type
    {
        Normal,
        Fire,
        Water,
        Grass,
        Electric,
        Ice,
        Fighting,
        Poison,
        Ground,
        Flying,
        Psychic,
        Bug,
        Rock,
        Ghost,
        Dark,
        Dragon,
        Steel,
        Fairy
    }

}
