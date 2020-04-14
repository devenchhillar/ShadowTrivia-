using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrivialQuiz.Models;

namespace TrivialQuiz.MongoDB
{
    public class UserService
    {
        private DataContext dataConnection;


        public UserService()
        {
            dataConnection = new DataContext();
        }

        public MongoCollection<User> getCollection()
        {
            var collection = this.dataConnection.getDatabase().GetCollection<User>("User");
            return collection;
        }

        public User getUserByEmail(String email)
        {
            return this.getCollection().FindOne(Query.EQ("EmailId", email));
        }
        public WriteConcernResult InsertRecord(User user)
        {

            return getCollection().Insert(user);
        }

    }
}