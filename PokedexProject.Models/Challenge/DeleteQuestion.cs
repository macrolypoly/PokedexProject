using PokedexProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models.Challenge
{
    public class DeleteQuestion
    {
        public int ChallengeId { get; set; }
        public string ChallengeName { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public virtual List<PokedexProject.Data.Question> ListOfQuestions { get; set; }
    }
}
