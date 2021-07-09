using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models
{
    public class TrainerItemsCreate
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int TrainerId { get; set; }
        public string TrainerName { get; set; }
        public int Count { get; set; }
    }
}
