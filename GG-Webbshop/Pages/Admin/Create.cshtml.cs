using GG_Webbshop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GG_Webbshop.Pages.Admin
{
    public class CreateModel : PageModel
    {
        public CreateModel()
        {

        }

        public IActionResult OnGet()
        {
            try
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

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return Page();
                    }
                    else
                    {
                        return RedirectToPage("/error");
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToPage("/error");
            }
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var values = new Dictionary<string, string>()
                 {
                    {"Id", $"{Product.Id}"},
                    {"Name", $"{Product.Name}"},
                    {"Price", $"{Product.Price}"},
                    {"Weight", $"{Product.Weight}"},
                    {"Description", $"{Product.Description}"},
                    {"Image", $"{Product.Image}"},
                    {"Category", $"{Product.Category}"},
                    {"CreateDate", $"{Product.CreateDate}"},
                    {"Stock", $"{Product.Stock}"},
                    {"Size", $"{Product.Size}"},
                    {"Brand", $"{Product.Brand}"}
                 };

            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            if (!String.IsNullOrEmpty(token))
            {
                RestClient client = new RestClient("https://localhost:44309/products/create");
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
                    return RedirectToPage("./Index");
                }
                else
                {
                    return RedirectToPage("/error");
                }
            }
            return Page();
        }
    }
}
