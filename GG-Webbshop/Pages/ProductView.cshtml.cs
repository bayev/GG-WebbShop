using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace GG_Webbshop.Pages
{
    public class ProductViewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public AllProductsResponseModel Product { get; set; }
     
        public ProductViewModel()
        {
        }

        public async Task<IActionResult> OnGetAsync() //HÄMTA PRODUCT HÄR
        {
            RestClient client;

            client = new RestClient($"https://localhost:44309/query/Get/{Id}");
            

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
                return RedirectToPage("/Error");
            }
            return Page();
        }
    }
}
