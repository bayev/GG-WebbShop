using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GG_Webbshop;
using GG_Webbshop.Helper;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using RestSharp;

namespace GG_Webbshop.Pages.Admin
{
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public AllProductsResponseModel Product { get; set; }
        [BindProperty(SupportsGet = true)]
        public string highlightedValue { get; set; }
        public EditModel()
        {

        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                id = Id;
                if (id == null)
                {
                    return RedirectToPage("/error");
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
                        return Page();
                    }
                    else
                    {
                        return RedirectToPage("/error");
                    }
                }
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/error");
            }
            
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            Product.Highlighted = highlightedValue == "Ja" ? true : false;
            id = Id;
            var values = new Dictionary<string, string>()
                 {
                    {"Id", $"{id}"},
                    {"Name", $"{Product.Name}"},
                    {"Price", $"{Product.Price}"},
                    {"Weight", $"{Product.Weight}"},
                    {"Description", $"{Product.Description}"},
                    {"Image", $"{Product.Image}"},
                    {"Category", $"{Product.Category}"},
                    {"CreateDate", $"{Product.CreateDate}"},
                    {"Stock", $"{Product.Stock}"},
                    {"Size", $"{Product.Size}"},
                    {"Brand", $"{Product.Brand}"},
                    {"Discount", $"{Product.Discount}"},
                    {"Highlighted", $"{Product.Highlighted}" }
                 };

            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            if (!String.IsNullOrEmpty(token))
            {
                RestClient client = new RestClient($"https://localhost:44309/products/update/{id}");
                RestRequest request = new RestRequest
                {
                    Method = Method.PUT
                };

                request.Parameters.Clear();
                request.AddHeader("Authorization", $"bearer {token}");
                request.AddJsonBody(values);
                request.AddParameter("application/json", values, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToPage("./index");
                }
                else
                {
                    return RedirectToPage("/error");
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
