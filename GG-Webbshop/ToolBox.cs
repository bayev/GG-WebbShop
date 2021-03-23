using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GG_Webbshop
{
    public static class ToolBox
    {
        public static string TokenName = "_Token";
        public static string LoggedInUserID { get; set; }
        public static string userID { get; set; }
        public static string ActiveRole { get; set; }
        public static bool IsValidEmail(string email)
        {

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
