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
using RestSharp;

namespace GG_Webbshop.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public AllProductsResponseModel Product { get; set; }
        public DeleteModel()
        {

        }
        public async Task<IActionResult> OnGetAsync(string id) //HÄMTA PRODUCT HÄR
        {
            id = Id;
            if (id == null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> OnPostAsync(string id)
        {
            id = Id;

            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            if (!String.IsNullOrEmpty(token))
            {
                RestClient client = new RestClient($"https://localhost:44309/products/delete/{id}");
                RestRequest request = new RestRequest
                {
                    Method = Method.DELETE
                };

                request.Parameters.Clear();
                request.AddHeader("Authorization", $"bearer {token}");

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToPage("./index");
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
