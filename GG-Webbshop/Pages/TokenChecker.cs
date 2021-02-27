using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG_Webbshop.Pages
{
    public static class TokenChecker
    {
        public static bool UserStatus { get; set; }
        
        public static void SetLoggedIn()
        {
            UserStatus = true;
        }
        public static void SetLoggedOut()
        {
            UserStatus = false;
        }
    }
}
