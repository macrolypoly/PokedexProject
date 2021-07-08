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
        [Key]
        public int PokemonId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Type2 { get; set; }
    }
}
