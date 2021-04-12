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
        public AllProductsResponseModel[] Product { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ProductId { get; set; }
        [BindProperty]
        public string Message { get; set; }
        public int ProductResultCount { get; set; }
        public SearchResultModel()
        {
        }
        public async Task<IActionResult> OnGetAsync() //HÄMTA PRODUCT HÄR
        {
            if(ProductId != null)
            {
                string token = null;
                string values = null;
                try
                {
                    byte[] tokenByte;
                    HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                    token = Encoding.ASCII.GetString(tokenByte);
                    values = ProductId;
                }
                catch (Exception)
                {
                    Message = "Du måste logga in för att lägga till varor i din kundvagn!";
                    return Page();
                }

                if (!String.IsNullOrEmpty(token))
                {
                    RestClient client1 = new RestClient("https://localhost:44309/cart/addtocart");
                    RestRequest request1 = new RestRequest
                    {
                        Method = Method.POST
                    };
                    request1.Parameters.Clear();
                    request1.AddHeader("Authorization", $"bearer {token}");
                    request1.AddJsonBody(values);

                    IRestResponse response1 = client1.Execute(request1);

                    if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Message = "Varan tillagd i varukorgen!";
                        return Page();
                    }
                    if (response1.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        Message = "Produkten är tillfälligt slut!";
                        return Page();
                    }
                }
                else
                {
                    Message = "Du måste logga in för att kunna börja shoppa loss! Registrera dig nu :)";
                    return Page();
                }
                return Page();
            }
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
