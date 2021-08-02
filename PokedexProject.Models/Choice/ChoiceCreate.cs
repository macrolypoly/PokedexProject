using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models.Choice
{
   public class ChoiceCreate
    {
        public Guid OwnerId { get; set; }
        public string ChoiceText { get; set; }
        public int QuestionId { get; set; }
        public bool MyAnswer { get; set; } = false;
        public int ChallengeId { get; set; }
    }
}
