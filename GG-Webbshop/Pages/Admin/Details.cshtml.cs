using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GG_Webbshop;

namespace GG_Webbshop.Pages.Admin
{
    public class DetailsModel : PageModel
    {

        public DetailsModel()
        {
            
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
