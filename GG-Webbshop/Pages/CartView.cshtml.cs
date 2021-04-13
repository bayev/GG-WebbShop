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

        [BindProperty(SupportsGet = true)]
        public string PlusMinus { get; set; }

        [BindProperty(SupportsGet = true)]
        public string c2pIdUpdate { get; set; }

        public decimal TotalPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PaymentMethod { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal VATprice { get; set; }
        [BindProperty(SupportsGet = true)]
        public UserLoginResponseModel user { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ShippingFee { get; set; }

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
                Message = "Du måste logga in för att kunna se dina valda varor.";
                return Page();
            }

            string IdUser = HttpContext.Session.GetString("Id");
            RestClient client1 = new RestClient($"https://localhost:44309/user/profile/{IdUser}");
            RestRequest request1 = new RestRequest
            {
                Method = Method.GET
            };
            request1.Parameters.Clear();
            request1.AddHeader("Authorization", $"bearer {token}");

            IRestResponse response1 = client1.Execute(request1);

            if (response1.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var model = UserLoginResponseModel.FromJsonSingle(response1.Content);
                user = model;
            }
            else
            {
                return NotFound();
            }
            if (PlusMinus != null)
            {

                RestClient client = new RestClient($"https://localhost:44309/cart/updateQuantity/{PlusMinus}/{c2pIdUpdate}");
                RestRequest request = new RestRequest
                {
                    Method = Method.PUT
                };
                request.Parameters.Clear();
                request.AddHeader("Authorization", $"bearer {token}");
                IRestResponse response = client.Execute(request);
                string responseContent = response.Content;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToPage("./cartview");
                }
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Produkten är slut i lager";
                    return Page();
                }
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Message = "Du som användare hittades inte i vår databas. Vänligen kontakta administratören";
                    return Page();
                }
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
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToPage("./cartview");
                }
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message2 = "Vi kunde inte radera produkten från din varukorg, vänligen kontakta administratören";
                    return Page();
                }
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Message = "Du som användare hittades inte i vår databas. Vänligen kontakta administratören";
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
                    decimal tempPrice = 0;

                    foreach (var item in c2pRM)
                    {
                        tempPrice = item.Price;


                        for (int i = 1; i < item.Amount; i++)
                        {
                            if (item.Amount == 1)
                                break;
                            if (i == item.Amount)
                                break;
                            else
                                item.Price += tempPrice;

                        }
                        TotalPrice += item.Price;
                    }

                    foreach (var item in c2pRM)
                    {
                        if (item.Discount != default)
                        {
                            TotalDiscount += item.Price * (item.Discount / 100);
                        }
                    }

                    VATprice = Math.Round((decimal)(TotalPrice - TotalDiscount) * (decimal)0.75, 2);
                    TotalPrice = Math.Round(TotalPrice, 2);
                    return Page();
                }
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Message = "Din varukorg är tom!";
                    return Page();
                }
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Message = "Användaren kunde inte hittas. Prova att logga in och ut igen!";
                }
                else
                {
                    Message = "Något gick heltokigt! kontakta administratören";
                    return Page();
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
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
                Message = "Du måste logga in för att kunna se dina valda varor.";
                return Page();
            }

            var values = new Dictionary<string, string>()
            {
                {"shippingAddress",$"{user.Billingaddress}"},
                {"paymentMethod", $"{PaymentMethod}"},
                {"totalAmount", $"{TotalPrice}"},
                { "shippingFee", $"{ShippingFee}"}
            };

            string IdUser = HttpContext.Session.GetString("Id");
            RestClient client = new RestClient($"https://localhost:44309/cart/placeOrder/");
            RestRequest request = new RestRequest
            {
                Method = Method.POST
            };
            request.Parameters.Clear();
            request.AddHeader("Authorization", $"bearer {token}");
            request.AddJsonBody(values);


            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToPage("./OrderView");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Message = "Produkten är slut i lager";
                return Page();
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Message = "Du som användare hittades inte i vår databas. Vänligen kontakta administratören";
                return Page();
            }
            return Page();
        }


    }
}
