using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models.Choice
{
    public class ChoiceListItem
    {
        public Guid OwnerId { get; set; }
        public int ChoiceId { get; set; }
        public string ChoiceText { get; set; }
        public int QuestionId { get; set; }
    }
}
