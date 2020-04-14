using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrivialQuiz.Models;

namespace TrivialQuiz
{
    public class TriviaService
    {
        DataContext dConnection = null;

        public TriviaService()
        {
            dConnection = new DataContext();
        }
        public MongoCollection<QuestionsAndCategory> getCollection()
        {
            var collection = dConnection.getDatabase().GetCollection<QuestionsAndCategory>("QuesAnswers");
            return collection;
        }
        public MongoCollection<QuestionsAndCategory> getPlayerStatusColection()
        {
            var collection = dConnection.getDatabase().GetCollection<QuestionsAndCategory>("PlayerStatus");
            return collection;
        }

        public ReturnType InsertTrivia(QuestionsAndCategory quesCat)
        {
            var result = getCollection().Insert<QuestionsAndCategory>(quesCat);

            if (result != null)
            {
                return new ReturnType(1);
            }
            else
            {
                return new ReturnType(0);
            }
        }
        public QuestionsAndCategory getQuestions(string userName)
        {
            Random rnd = new Random();
            List<Guid> listQuestionAnswered = new List<Guid>();
            listQuestionAnswered = GetAnsweredQuestionList(userName);

            List<QuestionsAndCategory> result = null;
            bool check = false;
            IEnumerable<QuestionsAndCategory> listQuestions;
            // get list of questions answered by user
            
            if (listQuestionAnswered!=null)
            {
                listQuestions = getCollection().FindAllAs<QuestionsAndCategory>().Where(x => !listQuestionAnswered.Contains(x.QuestionId));

            }
            else
            {
                listQuestions = getCollection().FindAllAs<QuestionsAndCategory>();
            }
                // exclude the list from the collections of questions


                // then pick a random quesiton

                long count = listQuestions.Count();
            if (count > 0)
            {
                var ran = (rnd.Next(1, Convert.ToInt32(count)));

                if (ran == count)
                {
                    result = listQuestions.Skip(ran - 1).Take(1).ToList();
                }
                else
                    result = listQuestions.Skip(ran).Take(1).ToList();

                if (result[0] != null)
                {
                    return result[0];
                }
                else
                    return null;
            }
            else
                return null;
            
        }
        public bool CheckAnswer(int quesId,string answerText)
        {
            var query = Query.And(Query.EQ("QuestionId", quesId));
            var result = getCollection().FindOneAs<QuestionsAndCategory>(query);
            if (result != null && result.CorrectAnswer.ToUpper() == answerText.ToUpper())
            {
                return true;
            }
            else
                return false;

        }
        public bool InsertPlayerStats(Guid questionId, string username, int score)
        {
            try
            {
                var query = Query.And(Query.EQ("UserName", username));
                var result = getPlayerStatusColection().FindOneAs<PlayerStatus>(query);

                if (result != null)
                {
                    result.ListQuestionGuid.Add(questionId);
                    result.score = score;
                    getPlayerStatusColection().Save(result);
                }
                else
                {
                    PlayerStatus platerStatus = new PlayerStatus();
                    platerStatus.ListQuestionGuid = new List<Guid>();
                    platerStatus.UserName = username;
                    platerStatus.ListQuestionGuid.Add(questionId);
                    platerStatus.score = score;
                    getPlayerStatusColection().Insert<PlayerStatus>(platerStatus);
                }
            }
            catch (Exception ex)
            { return false; }
            return true;
        }
        public List<Guid> GetAnsweredQuestionList(string uname)
        {
            var query = Query.And(Query.EQ("UserName", uname));
            var result = getPlayerStatusColection().FindOneAs<PlayerStatus>(query);
            if (result != null)
                return result.ListQuestionGuid;
            return null;
        }
        public int GetUserScore(string uname)
        {
            var query = Query.And(Query.EQ("UserName", uname));
            var result = getPlayerStatusColection().FindOneAs<PlayerStatus>(query);
            if (result != null)
                return result.score;
            return 0;
        }
        public bool ShowNextQuestion(string uname)
        {
            
            if (GetAnsweredQuestionList(uname) != null)
            {
                var count = GetAnsweredQuestionList(uname).Count();
                var allcount = getAllQuestions("");
                if (count <= allcount)
                {
                    return false;
                }
               
            }
            return true;

        }

        public int getAllQuestions(string category)
        {
            if (category == null || category == "")
            {
                return getCollection().FindAllAs<QuestionsAndCategory>().ToList().Count;
            }
            else
                return getCollection().FindAllAs<QuestionsAndCategory>().Where(x => x.selectedTags.Contains(category)).ToList().Count;
            
        }

        public bool resetTrivia(string uname)
        {
            var result = getPlayerStatusColection().FindOneAs<PlayerStatus>(Query.And(Query.EQ("UserName",uname)));
            if (result != null)
            {
                result.ListQuestionGuid.Clear();
                result.score = 0;
                getPlayerStatusColection().Save(result);
                return true;
            }
            else
                return false;
        }


    }
}