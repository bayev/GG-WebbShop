using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GG_Webbshop.Helper;
using GG_Webbshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GG_Webbshop.Pages
{
    public class LoginViewModel : PageModel
    {

        ProductAPI _api = new ProductAPI();
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }


        public string SessionInfoToken { get; private set;}
        
        public void OnGet()
        {
        }

        public async Task<IActionResult>OnPostAsync(string userName, string password)
        {
            userName = UserName;
            password = Password;


            HttpClient client = _api.Initial();

            var values = new Dictionary<string, string>()
                 {
                    {"user", $"{userName}"},
                    {"password", $"{password}"}
                 };

            string payload = JsonConvert.SerializeObject(values);

            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("auth/login", content);

            string request = response.Content.ReadAsStringAsync().Result;

            LoginResponseModel result = JsonConvert.DeserializeObject<LoginResponseModel>(request);


            // Set value in session
            byte[] tokenInByte = Encoding.ASCII.GetBytes(result.Token);
            HttpContext.Session.Set(ToolBox.TokenName, tokenInByte);



            //NEXT: Deserialize token från result. Använd till productscontroller på något vis, ev. user.


            return RedirectToPage("/admin/index");

        }
    }
}
