using System;
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
        public List<Pokemon> PokemonCaught { get; set; }
        public List<Item> ListOfItems { get; set; }
        public DateTimeOffset ProfileCreated { get; set; }

    }
}
