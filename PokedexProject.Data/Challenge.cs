using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Data
{
    public class Challenge
    {
        public Guid OwnerId { get; set; }
        [Key]
        public int ChallengeId { get; set; }
        public string ChallengeName { get; set; }
        [ForeignKey(nameof(Route))]
        public int RouteId { get; set; }
        public virtual PokeRoute Route { get; set; }
        public virtual List<Question> ListOfQuestions { get; set; }
        public virtual List<Choice> Choices { get; set; }
    }
}
