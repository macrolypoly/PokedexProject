using PokedexProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models.Route
{
    public class RouteDeleteItem
    {
        public int RouteId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public ICollection<PokeItem> ListOfItems { get; set; }
    }
}
