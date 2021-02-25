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
        public string QueryString { get; set; }
        [BindProperty(SupportsGet = true)]
        public AllProductsResponseModel Product { get; set; }
        public SearchResultModel()
        {
        }

        public async Task<IActionResult> OnGetAsync(string queryString) //HÄMTA PRODUCT HÄR
        {
            queryString = QueryString;
            if (queryString == null)
            {
                return NotFound();
            }
            //byte[] tokenByte;
            //HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            //string token = Encoding.ASCII.GetString(tokenByte);

                RestClient client = new RestClient($"https://localhost:44309/query/search/{queryString}");
                RestRequest request = new RestRequest
                {
                    Method = Method.GET
                };
                request.Parameters.Clear();

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
            
            return Page();
        }
    }
}
