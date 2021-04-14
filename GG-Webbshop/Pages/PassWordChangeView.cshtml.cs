using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace GG_Webbshop.Pages.Shared
{
    public class PassWordChangeViewModel : PageModel
    {
        [BindProperty]
        public string PasswordCurrent { get; set; }
        [BindProperty]
        public string Password1 { get; set; }
        [BindProperty]
        public string Password2 { get; set; }
        [BindProperty]
        public string PasswordMessage { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (Password1 != null)
            {
                if (Password1 == Password2)
                {
                    byte[] tokenByte;
                    HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                    string token = Encoding.ASCII.GetString(tokenByte);

                    var valuesPass = new Dictionary<string, string>() //Payload
                        {
                            {"currentPassword", $"{PasswordCurrent}"},
                            {"newPassword", $"{Password1}"},
                            {"confirmNewPassword", $"{Password2}"}
                        };

                    RestClient clientPass = new RestClient($"https://localhost:44309/auth/changepassword");
                    RestRequest requestPass = new RestRequest
                    {
                        Method = Method.POST
                    };
                    requestPass.Parameters.Clear();
                    requestPass.AddHeader("Authorization", $"bearer {token}");
                    requestPass.AddJsonBody(valuesPass);
                    IRestResponse responsePass = clientPass.Execute(requestPass);

                    if (responsePass.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ErrorMessage = "D� var det klart!";
                        return Page();
                    }
                    if (responsePass.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        PasswordMessage = "Ditt nuvarande l�senord st�mmer inte, f�rs�k igen!";
                        return Page();
                    }
                    else
                    {
                        ErrorMessage = "N�got gick helgalet, kontakta administrat�ren eller f�rs�k igen!";
                        return Page();
                    }
                }
                else
                {
                    PasswordMessage = "L�senorden matchade inte, testa igen!";
                    return Page();
                }
                
            }
            ErrorMessage = "Du verkst�llde inga �ndringar";
            return Page();
        }
    }
}
