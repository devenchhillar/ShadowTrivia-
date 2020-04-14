using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrivialQuiz.Models;

namespace TrivialQuiz.Controllers
{
    public class HomeController : Controller
    {
        public TrivialCategory triviaCat = new TrivialCategory();
        public TriviaService triviaService = new TriviaService();
        public List<SelectListItem> dt = new List<SelectListItem>();
        public string UserName { get; set; }
        public QuestionsAndCategory qa = new QuestionsAndCategory();
        public PlayerStatus pStatus = new PlayerStatus();
        public ActionResult Main()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            //set Dictionary

            dt = triviaCat.getCategory();
            // this.UserName = Session["UserName"].ToString();
            ViewBag.Message = "Start Playing/Creating Questions";
            ViewData["ListItems"] = dt;
            ViewBag.Score = triviaService.GetUserScore(Session["UserName"].ToString());
            return View(qa);
        }

        [HttpPost]
        [AllowAnonymous]
        public  ActionResult CreateQuestion(QuestionsAndCategory model)
        {
            model.QuestionId = Guid.NewGuid();
            ReturnType rt = triviaService.InsertTrivia(model);
            if (rt.value == 0)
            {
              //return new eM
            }

            return RedirectToAction("Main", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Trivia()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            TriviaQuestion trivia = new TriviaQuestion();
            ViewBag.Score = triviaService.GetUserScore(Session["UserName"].ToString());

            QuestionsAndCategory ques = triviaService.getQuestions(Session["UserName"].ToString());
            if (ques == null)
            {
                ViewBag.Message = "No questions left. Please create more questions.";
                ViewBag.ShowNextButton = "invisible";
            }
            else
            {
                
                trivia.QuestionText = ques.Question;
                trivia.QuestionID = ques.QuestionId;
                trivia.CorrectAnswer = ques.CorrectAnswer;
                ViewBag.ShowNextButton = "";
               
            }
            return View(trivia);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult TriviaQuestion(TriviaQuestion triviaQues)
        {
            int score = triviaService.GetUserScore(Session["UserName"].ToString());

            if (triviaQues.Answer == triviaQues.CorrectAnswer)
            {
                score += 4;
            }
            else
            {
                score = score - 1;
            }
            triviaService.InsertPlayerStats(triviaQues.QuestionID, Session["UserName"].ToString(), score);

            ViewBag.Score = score.ToString();
            return RedirectToAction("Trivia", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Reset()
        {
            string uname = Session["UserName"].ToString();
            if (triviaService.resetTrivia(uname))
            {
                ViewBag.Message = "Trivia Successfully reset";
            }
            return RedirectToAction("Trivia", "Home");
        }
        [HttpGet]
       public ActionResult LogOff()
        {

            return RedirectToAction("Index", "Account");
        }

    }
}