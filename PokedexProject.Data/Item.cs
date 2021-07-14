using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Data
{
   public class Item
    {
        public Guid OwnerId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }

       public ICollection<PokeRoute> ListOfRoutes { get; set; }
        public Item()
        {
            ListOfRoutes = new HashSet<PokeRoute>();
        }
    }
}
