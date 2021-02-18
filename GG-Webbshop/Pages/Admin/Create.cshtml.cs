using GG_Webbshop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GG_Webbshop.Pages.Admin
{
    public class CreateModel : PageModel
    {
        ProductAPI _api = new ProductAPI();
        public CreateModel()
        {

        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //FUNKAR EJ JUST NU :(
            HttpClient client = _api.Initial();
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
            string payload = JsonConvert.SerializeObject(values);

            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync("Products/create", content);

            return Page();
            //return RedirectToPage("./Index");

        }
    }
}
