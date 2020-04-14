using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrivialQuiz.Models
{
    public class TriviaQuestion
    {
        public Guid QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string Answer { get; set; }
        public string CorrectAnswer { get; set; }
    }
}