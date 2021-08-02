﻿using PokedexProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models.Question
{
   public class QuestionCreate
    {
        public Guid OwnerId { get; set; }
        [Key]
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int ChallengeId { get; set; }
        public int AnswerId { get; set; }
        public virtual List<PokedexProject.Data.Choice> Choices { get; set; }
    }
}
