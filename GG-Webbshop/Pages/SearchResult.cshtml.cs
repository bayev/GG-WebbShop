using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GG_Webbshop.Pages
{
    public class SearchResultModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public AllProductsResponseModel Product { get; set; }
        public SearchResultModel()
        {
        }

        public async Task<IActionResult> OnGetAsync(string id) //HÄMTA PRODUCT HÄR
        {
            id = Id;
            if (id == null)
            {
                return await NotFound();
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
                    return await NotFound();
                }
            }
            return Page();
        }

        Task<IActionResult> NotFound()
        {
            throw new NotImplementedException();
        }
    }
}
