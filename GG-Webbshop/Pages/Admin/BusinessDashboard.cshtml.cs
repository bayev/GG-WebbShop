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
    public class BusinessDashboardModel : PageModel
    {

       
        [BindProperty]
        public int AllSales { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                byte[] tokenByte;
                HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                string token = Encoding.ASCII.GetString(tokenByte);
                if (!String.IsNullOrEmpty(token))
                {
                    RestClient client = new RestClient("https://localhost:44309/Sales/all");
                    RestRequest request = new RestRequest
                    {
                        Method = Method.GET
                    };
                    request.Parameters.Clear();
                    request.AddHeader("Authorization", $"bearer {token}");

                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //Skicka in ny responsemodel här, ny data.

                        //var model = SalesResponseModel.FromJson(response.Content);
                        //Sales = model;
                        //AllSales = Sales.Length;
                        return Page();
                    }
                    else
                    {
                        return RedirectToPage("/error");
                    }
                }
                return RedirectToPage("/index");
            }
            catch (Exception)
            {
                return RedirectToPage("/error");
            }
        }
    }
}
