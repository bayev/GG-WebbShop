using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GG_Webbshop.Models;
using GG_Webbshop.Models.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace GG_Webbshop.Pages
{
    public class UserPageModel : PageModel
    {
       
        [BindProperty(SupportsGet = true)]
        public UserLoginResponseModel user { get; set; }
        [BindProperty(SupportsGet = true)]
        public OrderResponseModel[] Orders { get; set; }

        public async Task<IActionResult> OnGetAsync() 
        {
            

            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            string Id = HttpContext.Session.GetString("Id");
            

            if (!String.IsNullOrEmpty(token))
            {
                RestClient client = new RestClient($"https://localhost:44309/user/profile/{Id}");
                RestRequest request = new RestRequest
                {
                    Method = Method.GET
                };
                request.Parameters.Clear();
                request.AddHeader("Authorization", $"bearer {token}");

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var model = UserLoginResponseModel.FromJsonSingle(response.Content);
                    user = model;

                    RestClient client1 = new RestClient($"https://localhost:44309/cart/getallorders/{Id}");
                    RestRequest request1 = new RestRequest
                    {
                        Method = Method.GET
                    };
                    request1.Parameters.Clear();
                    request1.AddHeader("Authorization", $"bearer {token}");

                    IRestResponse response1 = client1.Execute(request1);
                    if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var allOrders = OrderResponseModel.FromJson(response1.Content);
                        Orders = allOrders;
                        return Page();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return Page();
        }
    }
}
