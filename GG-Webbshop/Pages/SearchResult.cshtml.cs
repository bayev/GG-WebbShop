using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System.Threading.Tasks;

namespace GG_Webbshop.Pages
{
    public class SearchResultModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public AllProductsResponseModel[] Product { get; set; }
        public string Message { get; set; }
        public int ProductResultCount { get; set; }
        public SearchResultModel()
        {
        }

        public async Task<IActionResult> OnGetAsync() //HÄMTA PRODUCT HÄR
        {
            RestClient client;
            if (string.IsNullOrEmpty(Id))
            {
                client = new RestClient($"https://localhost:44309/query/all");
            }
            else
            {
                client = new RestClient($"https://localhost:44309/query/search/{Id}");
            }

            
            RestRequest request = new RestRequest
            {
                Method = Method.GET
            };
            request.Parameters.Clear();

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var model = AllProductsResponseModel.FromJson(response.Content);
                Product = model;
                ProductResultCount = Product.Length;
                if (Product.Length == 0)
                {
                    Message = "Inga produkter hittade";
                    ProductResultCount = 0;
                }

            }
            else
            {
                return NotFound();
            }
            return Page();
        }
    }
}
