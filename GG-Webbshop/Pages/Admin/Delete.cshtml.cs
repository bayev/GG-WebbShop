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

namespace GG_Webbshop.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        ProductAPI _api = new ProductAPI();
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public DeleteModel()
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

        public async Task<IActionResult> OnPostAsync(string id) //DELETE PRODUCT HÄR
        {
            id = Id;
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.DeleteAsync($"products/delete/{Id}");
            if (id == null)
            {
                return NotFound();
            }




            return RedirectToPage("./Index");
        }
    }
}
