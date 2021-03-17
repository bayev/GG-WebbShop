using GG_Webbshop.Helper;
using GG_Webbshop.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GG_Webbshop.Pages
{
    public class RegisterPageModel : PageModel
    {
        ProductAPI _api = new ProductAPI();
        public string ErrorMessage { get; set; }
        public string MessageUserName { get; set; }
        public string MessageMail { get; set; }
        [BindProperty]
        public string MailMatch { get; set; }
        [BindProperty]
        public RegisterModel User { get; set; }
        public void OnGet()
        {
        }

        public string ValidMailMessage { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            bool validMailFormat = ToolBox.IsValidEmail(User.Email);
            if (!validMailFormat)
            {
                MessageMail = "V�nligen ange en giltig e-post";
                return Page();
            }

            bool matchingMails = User.Email.ToUpper() == MailMatch.ToUpper() ? true : false;
            if (!matchingMails)
            {
                MessageMail = "F�lten f�r e-postadress m�ste matcha, testa igen";
                return Page();
            }

            HttpClient client = _api.Initial();

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
            string payload = JsonConvert.SerializeObject(values);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("auth/register", content);

            string request = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                if (request == "Username in use")
                {
                    MessageUserName = "Anv�ndarnamnet anv�nds redan, testa ett annat";
                    return Page();
                }
                if (request == "E-mail in use")
                {
                    MessageMail = "Mailen anv�nds redan, testa en annan";
                    return Page();
                }
                if (request == "Registration failed")
                {
                    ErrorMessage = "Det gick inte att registrera en anv�ndare just nu," +
                                   " v�nligen f�rs�k igen senare eller kontakta " +
                                   "systemadministrat�ren p� ggwebbshop@gmail.com s� ordnar vi detta";
                }
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ToolBox.userID = request;
                return RedirectToPage("/RegisterConfirmation");
            }
            else
            {
                ErrorMessage = "Det gick inte att registrera en anv�ndare just nu," +
                               " v�nligen f�rs�k igen senare eller kontakta" +
                               " systemadministrat�ren p� ggwebbshop@gmail.com s� ordnar vi detta";
                return Page();
            }
        }
    }

}
