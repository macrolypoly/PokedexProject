using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models.Route
{
    public class RouteChallenge
    {
        public int ChallengeId { get;set; }
        public string ChallengeName { get; set; }
        public List<PokedexProject.Data.Question> ListOfQuestions { get; set; }
        public List<PokedexProject.Data.Choice> Choices { get; set; }
        public List<ChallengeChoice> UserSelected { get; set; }
        public bool Pass { get; set; }
    }
}
