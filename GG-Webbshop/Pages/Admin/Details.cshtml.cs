using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GG_Webbshop;
using System.Text;
using RestSharp;

namespace GG_Webbshop.Pages.Admin
{
    public class DetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public AllProductsResponseModel Product { get; set; }
        public DetailsModel()
        {

        }

        public async Task<IActionResult> OnGetAsync(string id) //HÄMTA PRODUCT HÄR
        {
            id = Id;

            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            if (!String.IsNullOrEmpty(token))
            {
                RestClient client = new RestClient($"https://localhost:44309/products/get/{id}");
                RestRequest request = new RestRequest
                {
                    Method = Method.GET
                };
                request.Parameters.Clear();
                request.AddHeader("Authorization", $"bearer {token}");

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var model = AllProductsResponseModel.FromJsonSingle(response.Content);
                    Product = model;
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
