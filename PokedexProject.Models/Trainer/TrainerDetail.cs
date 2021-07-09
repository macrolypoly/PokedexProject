using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models
{
    public class TrainerDetail
    {
        public Guid OwnerId { get; set; }
        public int TrainerId { get; set; }
        public string TrainerName { get; set; }
        public DateTimeOffset ProfileCreated { get; set; }
    }
}
