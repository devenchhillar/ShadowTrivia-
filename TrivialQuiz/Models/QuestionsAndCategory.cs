using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrivialQuiz.Models;


namespace TrivialQuiz.Models
{
   
    public class QuestionsAndCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string UserName { get; set; }

        public Guid QuestionId { get; set; }
        public string Question { get; set; }
        //public string [] Options { get; set; }
        public string CorrectAnswer { get; set; }
        public string selectedTags { get; set; }
        public bool bretrieved { get; set; }

       

    }
}