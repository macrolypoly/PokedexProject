using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models
{
    public class TrainerCreate
    {
        [Key]
        public Guid OwnerId { get; set; }
        [Required]
        public int TrainerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTimeOffset ProfileCreated { get; set; }
    }
}
