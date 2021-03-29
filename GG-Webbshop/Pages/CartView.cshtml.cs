using GG_Webbshop.Models.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GG_Webbshop.Pages
{
    public class CartViewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public c2pResponseModel[] c2pRM { get; set; }
        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public string Message2 { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            
            string token = null;
            try
            {
                byte[] tokenByte;
                HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                token = Encoding.ASCII.GetString(tokenByte);
            }
            catch (Exception)
            {
                Message = "Du m�ste logga in f�r att kunna se dina valda varor.";
                return Page();
            }
            if (Id != null)
            {
                RestClient client = new RestClient($"https://localhost:44309/cart/deletefromcart/{Id}");
                RestRequest request = new RestRequest
                {
                    Method = Method.DELETE
                };
                request.Parameters.Clear();
                request.AddHeader("Authorization", $"bearer {token}");

                IRestResponse response = client.Execute(request);
                string responseContent = response.Content;
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToPage("./cartview");
                }
                if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message2 = "Vi kunde inte radera produkten fr�n din varukorg, v�nligen kontakta administrat�ren";
                    return Page();
                }
                if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Message = "Du som anv�ndare hittades inte i v�r databas. V�nligen kontakta administrat�ren";
                    return Page();
                }
            }
            if (!String.IsNullOrEmpty(token))
            {
                RestClient client = new RestClient($"https://localhost:44309/cart/getcart");
                RestRequest request = new RestRequest
                {
                    Method = Method.GET
                };
                request.Parameters.Clear();
                request.AddHeader("Authorization", $"bearer {token}");

                IRestResponse response = client.Execute(request);
                string responseContent = response.Content;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var model = c2pResponseModel.FromJson(response.Content);
                    c2pRM = model;
                    return Page();
                }
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Din varukorg �r tom!";
                    return Page();
                }
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Message = "Anv�ndaren kunde inte hittas. Prova att logga in och ut igen!";
                }
                else
                {
                    Message = "N�got gick heltokigt! kontakta administrat�ren";
                    return Page();
                }
            }
            return Page();
        }
    }
}
