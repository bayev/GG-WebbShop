using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG_Webbshop.Pages.Admin
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public IFormFile UploadFile { get; set; }
        [BindProperty]
        public string Message { get; set; }
        [BindProperty(SupportsGet = true)]
        public string highlightedValue { get; set; }
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
        public async Task<IActionResult> OnPostAsync()
        {            
            if (UploadFile != null)
            {              
                var file = "./wwwroot/img/" + UploadFile.FileName;
                var fileNameDoubleCheck = Directory.GetFiles("./wwwroot/img/");
                foreach (var item in fileNameDoubleCheck)
                {
                    if (item == file)
                    {
                        Message = "Bilden är redan uppladdad! Byt filnamn eller välj en annan";
                        return Page();
                    }
                }

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    Product.Image = UploadFile.FileName;
                    await UploadFile.CopyToAsync(fileStream);
                }
            }
            Product.Highlighted = highlightedValue == "Ja" ? true : false;
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
                    {"Brand", $"{Product.Brand}"},
                    {"Highlighted", $"{Product.Highlighted}" }
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
