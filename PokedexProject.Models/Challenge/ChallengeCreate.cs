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
        public Guid OwnerId { get; set; }
        public int ChallengeId { get; set; }
        public string ChallengeName { get; set; }
        public ICollection<PokedexProject.Data.Question> ListOfQuestions { get; set; }
    }
}
