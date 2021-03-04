using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace GG_Webbshop.Pages.Admin
{
    public class RegisterAdminViewModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        public string ValidMailMessage { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                byte[] tokenByte;
                HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                string token = Encoding.ASCII.GetString(tokenByte);
                var values = new Dictionary<string, string>()
                 {
                    {"username", $"{User.Username}"},
                    {"email", $"{User.Email}"},
                    {"password", $"{User.Password}"},
                 };
                bool validMail = ToolBox.IsValidEmail(User.Email);
                if (!validMail)
                {
                    ValidMailMessage = "Ange en riktig e-post, tack!";
                    return Page();
                }
                RestClient client = new RestClient("https://localhost:44309/auth/adminregister");
                RestRequest request = new RestRequest
                {
                    Method = Method.POST
                };
                request.Parameters.Clear();
                request.AddHeader("Authorization", $"bearer {token}");
                request.AddJsonBody(values);

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TokenChecker.UserStatus = true;
                    return RedirectToPage("./index");
                }
                else
                {
                    TokenChecker.UserStatus = false;
                    return RedirectToPage("/error");
                }
            }
            catch (Exception)
            {

                return RedirectToPage("/error");
            }
            

        }
    }
}
