using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public virtual ICollection<Question> ListOfQuestions { get; set; }
        public Challenge()
        {
            ListOfQuestions = new HashSet<Question>();
        }
    }
}
