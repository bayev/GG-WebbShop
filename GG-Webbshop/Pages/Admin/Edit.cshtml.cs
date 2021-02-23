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

namespace GG_Webbshop.Pages.Admin
{
    public class EditModel : PageModel
    {
        ProductAPI _api = new ProductAPI();
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public EditModel()
        {

        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(string id) //HÄMTA PRODUCT HÄR
        {
            id = Id;
            if (id == null)
            {
                return NotFound();
            }
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"Products/get/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;

                Product = JsonConvert.DeserializeObject<Product>(result);
            }

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            id = Id;

            HttpClient client = _api.Initial();
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
                    {"Brand", $"{Product.Brand}"}
                 };
            string payload = JsonConvert.SerializeObject(values);

            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"Products/update/{id}", content);

            return RedirectToPage("./Index");

        }
    }
}
