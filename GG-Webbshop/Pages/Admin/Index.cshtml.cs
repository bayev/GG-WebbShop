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
using System.Net.Http.Headers;
using RestSharp;

namespace GG_Webbshop.Pages.Admin
{
    public class IndexModel : PageModel
    {
        ProductAPI _api = new ProductAPI();
        public IndexModel()
        {
           



        }

        public IList<Product> Product { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {

            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            if (!String.IsNullOrEmpty(token))
            {
                RestClient client = new RestClient("https://localhost:44309/products/all");
                RestRequest request = new RestRequest
                {
                    Method = Method.GET
                };
                request.Parameters.Clear();
                request.AddHeader("Authorization", $"bearer {token}");

                IRestResponse response = client.Execute(request);


                //var payload = new
                //{
                //    user = "admin",
                //    password = "AdminPass1!"

                //};

                //request.AddParameter("application/json", payload, ParameterType.RequestBody);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var model = AllProductsResponseModel.FromJson(response.Content);


                    foreach (var product in model)
                    {

                        string x = product.Description;

                    }
                }
                else
                {



                }
            }

            return Page();
        }
        

    }
}
