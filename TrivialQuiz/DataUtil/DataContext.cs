using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrivialQuiz.Models;

namespace TrivialQuiz
{
    public class DataContext
    {
        private string connectionString { get; set; }
        private MongoDatabase _database = null;
        private MongoClient _client = null;
        private MongoServer server = null;

        public DataContext()
        {
            this.setConnection();
        }
        public bool setConnection()
        {
            try
            {
                connectionString = "mongodb://" + Properties.Settings.Default.DBConnection;
                _client = new MongoClient(connectionString);
                server = _client.GetServer();
                _database = server.GetDatabase(Properties.Settings.Default.ConnectionDatabase);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public MongoDatabase getDatabase()
        {
            return _database;
        }
        

    }
}