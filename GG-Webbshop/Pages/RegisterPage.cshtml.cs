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

        public bool PrivacyChecker { get; set; }
        public void OnGet()
        {
        }
     
        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Email == null || MailMatch == null)
            {
                User.Email = "a";
                MailMatch = "b";
            }
            bool validMailFormat = ToolBox.IsValidEmail(User.Email);
            if (!validMailFormat)
            {
                MessageMail = "Vänligen ange en giltig e-post";
                return Page();
            }

            bool matchingMails = User.Email.ToUpper() == MailMatch.ToUpper() ? true : false;
            if (!matchingMails)
            {
                MessageMail = "Fälten för e-postadress måste matcha, testa igen";
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
                if(request == "wrong username")
                {
                    MessageUserName = "Vänligen välj ett användarnamn som inte innehåller mellanslag";
                    return Page();
                }
                if (request == "Username in use")
                {
                    MessageUserName = "Användarnamnet används redan, testa ett annat";
                    return Page();
                }
                if (request == "E-mail in use")
                {
                    MessageMail = "Mailen används redan, testa en annan";
                    return Page();
                }
                if (request == "Registration failed")
                {
                    ErrorMessage = "Det gick inte att registrera en användare just nu," +
                                   " vänligen försök igen senare eller kontakta " +
                                   "systemadministratören på ggwebbshop@gmail.com så ordnar vi detta";
                }
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ToolBox.userID = request;
                return RedirectToPage("/RegisterConfirmation");
            }
            else
            {
                ErrorMessage = "Det gick inte att registrera en användare just nu," +
                               " vänligen försök igen senare eller kontakta" +
                               " systemadministratören på ggwebbshop@gmail.com så ordnar vi detta";
                return Page();
            }
        }
    }

}
