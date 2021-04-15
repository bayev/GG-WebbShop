using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG_Webbshop.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public AllProductsResponseModel Product { get; set; }
        public DeleteModel()
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
                return RedirectToPage("/index");
            }
            catch (Exception)
            {
                return RedirectToPage("/error");
            }


        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            id = Id;

            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            var file = Directory.GetFiles("./wwwroot/img/").Select(x => Product.Image).FirstOrDefault();

            if(file != null)
                System.IO.File.Delete($"./wwwroot/img/{file}");

            if (!String.IsNullOrEmpty(token))
            {
                RestClient client = new RestClient($"https://localhost:44309/products/delete/{id}");
                RestRequest request = new RestRequest
                {
                    Method = Method.DELETE
                };

                request.Parameters.Clear();
                request.AddHeader("Authorization", $"bearer {token}");

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToPage("./index");
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
