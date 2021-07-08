﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Data
{
    public class Trainer
    {
        public Guid OwnerId { get; set; }
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public ICollection<Pokemon> PokemonCaught { get; set; }
        public ICollection<Item> ListOfItems { get; set; }
        public DateTimeOffset ProfileCreated { get; set; }

        public Trainer()
        {
            PokemonCaught = new HashSet<Pokemon>();
            ListOfItems = new HashSet<Item>();
        }

    }
}
