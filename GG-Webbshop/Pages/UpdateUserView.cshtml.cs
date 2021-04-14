using GG_Webbshop.Helper;
using GG_Webbshop.Models;
using GG_Webbshop.Models.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public string MailMatch { get; set; }
        public string ErrorMessage { get; set; }
        public string MessageUserName { get; set; }
        public string MessageMail { get; set; }

        ProductAPI _api = new ProductAPI();


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


            var values = new Dictionary<string, string>() //Payload
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

            

            RestClient client1 = new RestClient($"https://localhost:44309/user/userupdate");
            RestRequest request1 = new RestRequest
            {
                Method = Method.PUT
            };

            byte[] updateTokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out updateTokenByte);
            string renewedToken = Encoding.ASCII.GetString(updateTokenByte);

            request1.Parameters.Clear();
            request1.AddHeader("Authorization", $"bearer {renewedToken}");
            request1.AddJsonBody(values);
            request1.AddParameter("application/json", values, ParameterType.RequestBody);

            IRestResponse response1 = client1.Execute(request1);

            

            string responseContent = response1.Content;

            if (response1.StatusCode == System.Net.HttpStatusCode.OK)
            {
                LoginResponseModel result = JsonConvert.DeserializeObject<LoginResponseModel>(responseContent);
                byte[] tokenInByte1 = Encoding.ASCII.GetBytes(result.Token);
                HttpContext.Session.Remove(ToolBox.TokenName);

                HttpContext.Session.Set(ToolBox.TokenName, tokenInByte1);
                HttpContext.Session.SetString("Id", ToolBox.LoggedInUserID);


                TokenChecker.UserName = user.Username;

                
                return RedirectToPage("./UserPage");
            }
            
            if (response1.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string result = JsonConvert.DeserializeObject<string>(responseContent);

                if (result == "Username in use")
                {
                    MessageUserName = "Användarnamnet används redan, välj ett annat.";
                    return Page();
                }
                if (result == "E-mail in use")
                {
                    MessageMail = "E-posten används redan, välj en annan.";
                    return Page();
                }
                if (result == "Error, user not found")
                {
                    ErrorMessage = "Det gick inte att registrera en användare just nu," +
                              " vänligen försök igen senare eller kontakta" +
                              " systemadministratören på ggwebbshop@gmail.com så ordnar vi detta";
                    return Page();
                }

                return Page();
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

