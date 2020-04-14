using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrivialQuiz.Models
{
    
    public class TrivialCategory
    {
        
        public string count = "1";
        public int val = 0;
        public List<SelectListItem> TriviaName = new List<SelectListItem>();
        public List<SelectListItem> getCategory()
        {
            string[] category = Properties.Settings.Default.Categories.Split(',');
            foreach (string choice in category)
            {
                TriviaName.Add(new SelectListItem { Text=choice,Value=val.ToString()});
                val++;
            }
             return TriviaName;
        }
    }
}