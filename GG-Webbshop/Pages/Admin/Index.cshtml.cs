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
using System.Text;
using System.Net.Http.Headers;

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
            // Get value in session
            byte[] tokenByte;


            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);

            string token = Encoding.ASCII.GetString(tokenByte);

            HttpClient client = _api.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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
