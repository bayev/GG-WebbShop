using Microsoft.AspNetCore.Http;
using RestSharp;
using System;
using System.Text;

namespace GG_Webbshop.Pages
{
    public static class TokenChecker
    {
        public static bool UserStatus { get; set; }
        public static string UserName { get; set; }
    }
}


