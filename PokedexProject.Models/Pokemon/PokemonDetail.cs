using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models
{
    public class PokemonDetail
    {
        public Guid OwnerId { get; set; }
        [Key]
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }
        public int Type { get; set; }
        public int Type2 { get; set; }
    }
}
