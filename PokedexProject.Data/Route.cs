﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Data
{
    public class Route
    {
        [Key]
        public int RouteId { get; set; }
        public List<Pokemon> RoutePokemon { get; set; }
        public List<Item> ListOfItems { get; set; }
        public string RouteName { get; set; }
    }
}
