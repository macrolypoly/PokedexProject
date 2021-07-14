using System;
using PokedexProject.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models.Route
{
    public class RouteListCreate
    {
        public int ItemId { get; set; }
        public int RouteId { get; set; }

        public string RouteName { get; set; }

        public List<PokeRoute> ListOfRoutes { get; set; }
    }
}
