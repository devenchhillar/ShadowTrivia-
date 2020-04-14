using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrivialQuiz.Models;
using MongoDB.Driver.Builders;
using TrivialQuiz.MongoDB;

namespace TrivialQuiz.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
        public static string message { get; set; }
        public HomeController hm = new HomeController();
        public UserService userService = new UserService();

        public AccountController()
        {
             
        }
     
        // Get: /Account/Index
        [AllowAnonymous]
        public ActionResult Index()
        {
            
            ViewBag.Message = message;
            
            return View();
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
          
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(User user)
        {
            string result = "";
                result = CheckLogin(user.EmailId, user.Password);
                if (result.Equals("granted"))
                {
                    Session["UserName"] = user.EmailId;
                    return RedirectToAction("Main", "Home");
                }
           
            message = result;
            return RedirectToAction("Index", "Account");

        }
        public string CheckLogin(string user, string pass)
        {
            try
            {

                var result = userService.getUserByEmail(user);
                if (result == null)
                {
                    return "Could not connect to the database,incorrect UserName. Please try again!";
                }
                if (result.Password == pass)
                {
                   return "granted";
                }
                else
                {
                    return "Wrong UserName/Password";
                }
            }
            catch (Exception ex)
            {
                return "Could not connect to the database. Please try again!";
            }
        }
        
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
       
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(User model)
        {
            //check if Exists
            string res = CheckIfExists(model);

            // If we got this far, something failed, redisplay form
            Session["UserName"] = model.EmailId;
            return RedirectToAction("Main", "Home");
        }
        public string CheckIfExists(User uName)
        {
            try
            {
                
                
                var result = userService.getUserByEmail(uName.EmailId);
                if (result != null)
                {
                    return "User already exists";
                }
                else
                {
                    if (userService.InsertRecord(uName)!= null)
                    {
                        return "Successfully Registered";
                    }
                    else { return "Failed to register! Contact Support"; }
                }
            }
            catch (Exception exe)
            {
                return "Exception Occured - "+exe.Message;
            }
        }

       
    }
}