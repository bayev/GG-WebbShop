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
    public class DeleteAdminViewModel : PageModel
    {

        public async Task<IActionResult> OnGet()
        {
            try
            {
                byte[] tokenByte;
                HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                string token = Encoding.ASCII.GetString(tokenByte);
                if (!String.IsNullOrEmpty(token))
                {
                    RestClient client = new RestClient("https://localhost:44309/products/all");
                    RestRequest request = new RestRequest
                    {
                        Method = Method.GET
                    };
                    request.Parameters.Clear();
                    request.AddHeader("Authorization", $"bearer {token}");

                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return Page();
                    }
                    else
                    {
                        return RedirectToPage("/error");
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToPage("/error");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            if (!String.IsNullOrEmpty(token))
            {
                RestClient client = new RestClient($"https://localhost:44309/auth/admindelete");
                RestRequest request = new RestRequest
                {
                    Method = Method.DELETE
                };

                request.Parameters.Clear();
                request.AddHeader("Authorization", $"bearer {token}");

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TokenChecker.UserStatus = false;
                    return RedirectToPage("/LogOut");
                    
                }
                else
                {
                    return RedirectToPage("/error");
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
