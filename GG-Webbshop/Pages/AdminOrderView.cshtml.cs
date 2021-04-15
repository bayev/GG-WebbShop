using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GG_Webbshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace GG_Webbshop.Pages.Admin
{
    public class AdminOrderViewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public OrderResponseModel[] Orders { get; set; }       

        public async Task<IActionResult> OnGetAsync()
        {

            try
            {
                byte[] tokenByte;
                HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                string token = Encoding.ASCII.GetString(tokenByte);
                if (!String.IsNullOrEmpty(token))
                {
                    RestClient client = new RestClient("https://localhost:44309/admin/allOrders");
                    RestRequest request = new RestRequest
                    {
                        Method = Method.GET
                    };
                    request.Parameters.Clear();
                    request.AddHeader("Authorization", $"bearer {token}");

                    IRestResponse response = client.Execute(request);
                    var model = OrderResponseModel.FromJson(response.Content);
                    Orders = model;
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
            return Page();
        }
    }
}
