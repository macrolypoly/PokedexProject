using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Data
{
    public class Choice
    {
        public Guid OwnerId { get; set; }
        [Key]
        public int ChoiceId { get; set; }
        public string ChoiceText { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public int ChallengeId { get; set; }
        public virtual Challenge Challenge { get; set; }

        public string MyAnswer { get; set; }
    }
}
