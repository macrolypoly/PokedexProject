using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Data
{
   public class Answer
    {
        public Guid OwnerId { get; set; }
        [Key]
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        //[ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }
        //public virtual Question Question { get; set; }
    }
}
