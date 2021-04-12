using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace GG_Webbshop.Pages
{
    public class CategoryResultPageModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string QueryString { get; set; }

        [BindProperty(SupportsGet = true)]
        public AllProductsResponseModel[] Product { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ProductId { get; set; }
        [BindProperty]
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(string QueryString) //HÄMTA PRODUCT HÄR
        {

         
            if (ProductId != null)
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
            if (QueryString != null)
            {
                

                RestClient client = new RestClient($"https://localhost:44309/Algorithm/category/{QueryString}");
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
                }

            }
            return Page();
        }
    }
}
