using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Data
{
    public enum PokeType
    {
        Normal = 1,
        Fire = 2,
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
        Fairy,
        None
    }
    public class Pokemon
    {
        public Guid OwnerId { get; set; }
        [Key]
        public int PokemonId { get; set; }
        [Required]
        public string PokemonName { get; set; }
        [Required]
        [Display(Name = "Pokemon Type")]
        public PokeType Type { get; set; }
        [Required]
        [Display(Name = "Second Type")]
        public PokeType Type2 { get; set; }
    }
   

}
