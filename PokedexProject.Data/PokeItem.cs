using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Data
{
   public class PokeItem
    {

        public Guid OwnerId { get; set; }
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }

       public ICollection<PokeRoute> ListOfRoutes { get; set; }
        public PokeItem()
        {
            ListOfRoutes = new HashSet<PokeRoute>();
        }
    }
}
