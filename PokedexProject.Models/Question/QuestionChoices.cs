using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models.Question
{
    public class QuestionChoices
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int ChoiceId { get; set; }
        public string ChoiceText { get; set; }
    }
}
