using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models
{
    public class ChallengeVM
    {
        public int ChallengeId { get; set; }
        public string ChallengeName { get; set; }
        public List<PokedexProject.Models.SelectListItem> ListOfChallenges { get; set; }
    }
}
