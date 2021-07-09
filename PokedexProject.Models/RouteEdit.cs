using PokedexProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models
{
    public class RouteEdit
    {
        public Guid OwnerId { get; set; }
        [Key]
        public int RouteId { get; set; }
        public ICollection<Pokemon> RoutePokemon { get; set; }
        public ICollection<Item> ListOfItems { get; set; }
        public string RouteName { get; set; }
    }
}
