using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Data
{
    public class Question
    {
        public Guid OwnerId { get; set; }
        [Key]
        public int QuestionId { get; set; }

        public string QuestionText { get; set; }
        [ForeignKey("Challenge")]
        public int ChallengeId { get; set; }
        public virtual Challenge Challenge { get; set; }
        public virtual List<Choice> Choices { get; set; }
        [ForeignKey("Answer")]
        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
       // public string UserSelected { get; set; }
    }
}
