using PokedexProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models
{
    public class PokemonEdit
    {
        public Guid OwnerId { get; set; }
        [Key]
        public int PokemonId {get;set;}
        [Required]
        public string PokemonName { get; set; }
        [Required]
        public PokeType Type { get; set; }
        [Required]
        public PokeType Type2 { get; set; }

    }
}
