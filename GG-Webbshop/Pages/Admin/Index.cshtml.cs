using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GG_Webbshop;
using GG_Webbshop.Helper;
using System.Net.Http;
using Newtonsoft.Json;

namespace GG_Webbshop.Pages.Admin
{
    public class IndexModel : PageModel
    {
        ProductAPI _api = new ProductAPI();
        public IndexModel()
        {
           
        }

        public IList<Product> Product { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Products/all");
            if (res.IsSuccessStatusCode)
            {
               var result = res.Content.ReadAsStringAsync().Result;

               Product = JsonConvert.DeserializeObject<IList<Product>>(result);
                
            }

            return Page();
        }
    }
}
