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
    public class TokenChecker : PageModel
    {
        //public bool CheckIfValid()
        //{

        //    bool validToken;
            
        //    byte[] tokenByte;
        //    HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
        //    string token = Encoding.ASCII.GetString(tokenByte);

        //    if (!String.IsNullOrEmpty(token))
        //        validToken = true;
        //    else
        //        validToken = false;

        //    return validToken;
        //}
        //public IActionResult LogOutSessionClear()
        //{

        //    HttpContext.Session.Clear();
        //    return RedirectToPage("/index");
        //}

    }
}
