using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public string Message2 { get; set; }
        public ProductViewModel()
        {
        }

        public async Task<IActionResult> OnGetAsync() 
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
                return Page();
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Message2 = "Produkten �r tillf�lligt slut!";
                return Page();
            }
            else
            {
                return RedirectToPage("/Error");
            }
          
        }
        public async Task<IActionResult> OnPostAsync()
        {
            string token = null;
            string values = null;
            try
            {
                byte[] tokenByte;
                HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                token = Encoding.ASCII.GetString(tokenByte);
                values = Product.Id.ToString();
            }
            catch (Exception)
            {
                Message2 = "Du m�ste logga in f�r att l�gga till varor i din kundvagn!";
                return Page();
            }
            
            if (!String.IsNullOrEmpty(token))
            {
                RestClient client = new RestClient("https://localhost:44309/cart/addtocart");
                RestRequest request = new RestRequest
                {
                    Method = Method.POST
                };
                request.Parameters.Clear();
                request.AddHeader("Authorization", $"bearer {token}");
                request.AddJsonBody(values);

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Message = "Varan �r tillagd i varukorgen!";
                    return RedirectToPage("./CartView");
                }
                if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message2 = "Produkten �r tillf�lligt slut!";
                    return Page();
                }
            }
            else
            {
                Message2 = "Du m�ste logga in f�r att kunna b�rja shoppa loss! Registrera dig nu :)";
                return Page();
            }
            return Page();
        }
    }
}
