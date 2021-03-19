using GG_Webbshop.Models.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GG_Webbshop.Pages
{
    public class UpdateUserViewModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public UserLoginResponseModel user { get; set; }

        [BindProperty(SupportsGet = true)]
        public RegisterModel UpdateUser { get; set; }
        [BindProperty]
        public string Password1 { get; set; }
        [BindProperty]
        public string Password2 { get; set; }
        [BindProperty]
        public string PasswordMessage { get; set; }
        [BindProperty]
        public string MailMatch { get; set; }
        public string ErrorMessage { get; set; }
        public string MessageUserName { get; set; }
        public string MessageMail { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            string Id = HttpContext.Session.GetString("Id");


            if (!String.IsNullOrEmpty(token))
            {
                RestClient client = new RestClient($"https://localhost:44309/user/profile/{Id}");
                RestRequest request = new RestRequest
                {
                    Method = Method.GET
                };
                request.Parameters.Clear();
                request.AddHeader("Authorization", $"bearer {token}");

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var model = UserLoginResponseModel.FromJsonSingle(response.Content);
                    user = model;
                }
                else
                {
                    return NotFound();
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(user.Email == null || MailMatch == null)
            {
                user.Email = "a";
                MailMatch = "b";
            }
            bool validMailFormat = ToolBox.IsValidEmail(user.Email);
            if (!validMailFormat)
            {
                MessageMail = "Vänligen ange en giltig e-post";
                return Page();
            }

            bool matchingMails = user.Email.ToUpper() == MailMatch.ToUpper() ? true : false;
            if (!matchingMails)
            {
                MessageMail = "Fälten för e-postadress måste matcha, testa igen";
                return Page();
            }




            var values = new Dictionary<string, string>()
                 {
                    {"username", $"{user.Username}"},
                    {"email", $"{user.Email}"},
                    {"password", $"{user.Password}"},
                    {"fullName", $"{user.Fullname}"},
                    {"billingAddress", $"{user.Billingaddress}"},
                    {"defaultShippingAddress", $"{user.Defaultshippingaddress}"},
                    {"country", $"{user.Country}"},
                    {"phone", $"{user.Phone}"}
                 };
            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            RestClient client = new RestClient($"https://localhost:44309/user/userupdate");
            RestRequest request = new RestRequest
            {
                Method = Method.PUT
            };

            request.Parameters.Clear();
            request.AddHeader("Authorization", $"bearer {token}");
            request.AddJsonBody(values);
            request.AddParameter("application/json", values, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            //if(Password1 != null)
            //{
            //    if(Password1 == Password2)
            //    {
            //        //kod för att ändra lösenord mot APIET
            //        return RedirectToPage("/UserPage");
            //    }
            //    else
            //    {
            //        PasswordMessage = "Lösenorden matchade inte, testa igen! Allt annat är dock uppdaterat";
            //        return Page();
            //    }
            //}
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TokenChecker.UserName = user.Username;
                return RedirectToPage("./UserPage");
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

