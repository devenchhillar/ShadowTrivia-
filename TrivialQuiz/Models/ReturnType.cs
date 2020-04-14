using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrivialQuiz.Models
{
    public class ReturnType
    {
        public static string Success = "Success";
        public static string Failed = "Failed";
        public static int SuccessValue = 1;
        public static int FailValue = 0;

        public int value;
        public string message;

        public ReturnType() { }

        public ReturnType(int value)
        {
            if (value == SuccessValue)
            {
                message = Success;
                this.value = SuccessValue;
            }
            else
            {
                message = Failed;
                this.value = FailValue;
            }
        }

    }
}