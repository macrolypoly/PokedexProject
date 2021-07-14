using System;
using PokedexProject.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PokedexProject.Models
{
   public class ItemCreate
    {
        public Guid OwnerId { get; set; }
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
