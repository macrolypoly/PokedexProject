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
        public virtual ICollection<Choice> Choices { get; set; }
        public Question()
        {
            Choices = new HashSet<Choice>();
        }
    }
}
