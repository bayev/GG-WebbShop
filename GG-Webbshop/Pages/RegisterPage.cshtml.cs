using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GG_Webbshop.Pages
{
    public class RegisterPageModel : PageModel
    {
        public void OnGet()
        {
        }

        [BindProperty]
        public User User { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var values = new Dictionary<string, string>()
                 {
                    {"Id", $"{User.Id}"},
                    {"Name", $"{User.FullName}"},
                    {"Addresses", $"{User.Addresses}"},
                    {"BillingAddress", $"{User.BillingAddress}"},
                    {"Country", $"{User.Country}"},
                    {"Email", $"{User.Email}"},
                    {"Phone", $"{User.Phone}"},
                    {"Password", $"{User.Password}"},
                    {"DefaultShippingAddress", $"{User.DefaultShippingAddress}"}
                 };

            RestClient client = new RestClient("https://localhost:44309/user/create");
            RestRequest request = new RestRequest
            {
                Method = Method.POST
            };
            request.Parameters.Clear();
            //request.AddHeader("Authorization", $"bearer {token}");
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
    }

}
