﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Data
{
    public class PokeRoute
    {
        public Guid OwnerId { get; set; }
        [Key]
        public int RouteId { get; set; }
        public virtual ICollection<Pokemon> RoutePokemon { get; set; }
        public virtual ICollection<PokeItem> ListOfItems { get; set; }
        public virtual ICollection<Challenge> ListOfChallenges { get; set; }
        public string RouteName { get; set; }

        public PokeRoute()
        {
            RoutePokemon = new HashSet<Pokemon>();
            ListOfItems = new HashSet<PokeItem>();
            ListOfChallenges = new HashSet<Challenge>();
        }
    }
}
