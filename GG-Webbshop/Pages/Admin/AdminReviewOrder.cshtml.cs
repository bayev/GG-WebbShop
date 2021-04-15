using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GG_Webbshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace GG_Webbshop.Pages
{
    public class AdminReviewOrderModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public OrderResponseModel orders { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            
            try
            {
                id = Id;
                if (id == null)
                {
                    return RedirectToPage("/error");
                }
                byte[] tokenByte;
                HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                string token = Encoding.ASCII.GetString(tokenByte);
                if (!String.IsNullOrEmpty(token))
                {
                    RestClient client = new RestClient($"https://localhost:44309/admin/GetOrderStatus/{id}");
                    RestRequest request = new RestRequest
                    {
                        Method = Method.GET
                    };
                    request.Parameters.Clear();
                    request.AddHeader("Authorization", $"bearer {token}");

                    IRestResponse response = client.Execute(request);
                    var model = OrderResponseModel.FromJsonSingle(response.Content);
                    orders = model;
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
            return Page();
        }
       

        public async Task<IActionResult> OnPostAsync(string id)
        {
            try
            {
                id = Id;
                if (id == null)
                {
                    return RedirectToPage("/error");
                }
                byte[] tokenByte;
                HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                string token = Encoding.ASCII.GetString(tokenByte);
                if (!String.IsNullOrEmpty(token))
                {

                    RestClient client = new RestClient($"https://localhost:44309/admin/changeOrderStatus/{id}");
                    RestRequest request = new RestRequest
                    {
                        Method = Method.PUT
                    };
                    request.Parameters.Clear();
                    request.AddHeader("Authorization", $"bearer {token}");

                    IRestResponse response = client.Execute(request);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
            return RedirectToPage("/AdminOrderView");

        }

    }
}
