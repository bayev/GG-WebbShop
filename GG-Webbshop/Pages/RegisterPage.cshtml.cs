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
        public string ValidMailMessage { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var values = new Dictionary<string, string>()
                 {
                    {"username", $"{User.Username}"},
                    {"email", $"{User.Email}"},
                    {"password", $"{User.Password}"},
                    {"fullName", $"{User.FullName}"},
                    {"billingAddress", $"{User.BillingAddress}"},
                    {"defaultShippingAddress", $"{User.DefaultShippingAddress}"},
                    {"country", $"{User.Country}"},
                    {"phone", $"{User.Phone}"}
                 };
            bool validMail = ToolBox.IsValidEmail(User.Email);
            if(!validMail)
            {
                ValidMailMessage = "Ange en riktig e-post, tack!";
                return Page();
            }
            RestClient client = new RestClient("https://localhost:44309/auth/register");
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
                TokenChecker.UserStatus = true;
                return RedirectToPage("/Index");
            }
            else
            {
                TokenChecker.UserStatus = false;
                return RedirectToPage("/index");
            }

        }
    }

}
