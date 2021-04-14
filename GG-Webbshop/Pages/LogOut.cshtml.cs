using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GG_Webbshop.Pages
{
    public class LogOutModel : PageModel
    {
        public IActionResult OnGet()
        {
            ToolBox.ActiveRole = null;
            TokenChecker.UserStatus = false;
            HttpContext.Session.Clear();
            return RedirectToPage("/index");
        }
    }
}
