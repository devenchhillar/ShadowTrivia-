using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrivialQuiz.Models
{
    public class PlayerStatus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public List<Guid> ListQuestionGuid { get; set; }
        public string UserName { get; set; }
        public int score { get; set; }

    }
}