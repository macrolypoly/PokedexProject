using PokedexProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models.Challenge
{
    public class ChallengeCreate
    {
        public string ChallengeName { get; set; }
        public int RouteId { get; set; }
    }
}
