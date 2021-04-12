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
    public class OrderViewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public UserLoginResponseModel user { get; set; }

        [BindProperty(SupportsGet = true)]
        public OrderResponseModel order { get; set; }

        [BindProperty]
        public string Message { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            string token = null;
            try
            {
                byte[] tokenByte;
                HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                token = Encoding.ASCII.GetString(tokenByte);
            }
            catch (Exception)
            {
                Message = "Du måste logga in för att kunna se dina ordrar.";
                return Page();
            }

            string IdUser = HttpContext.Session.GetString("Id");
            RestClient client1 = new RestClient($"https://localhost:44309/cart/getOrder/{IdUser}");
            RestRequest request1 = new RestRequest
            {
                Method = Method.GET
            };
            request1.Parameters.Clear();
            request1.AddHeader("Authorization", $"bearer {token}");

            IRestResponse response1 = client1.Execute(request1);

            if (response1.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var model = OrderResponseModel.FromJsonSingle(response1.Content);
                order = model;
            }
            else
            {
                Message = "Vi kunde inte hämta din order, vänligen kontakta administratören.";
                return Page();
            }
            return Page();
        }
    }
}
