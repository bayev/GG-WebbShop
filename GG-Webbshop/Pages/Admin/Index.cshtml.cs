using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GG_Webbshop.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public string TotalCash { get; set; }
        public string TotalMembers { get; set; }
        public string TotalOrders { get; set; }
        public string TotalSoldProducts { get; set; }
        public AllProductsResponseModel[] Product { get; set; }
        public IndexModel()
        {

        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                byte[] tokenByte;
                HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                string token = Encoding.ASCII.GetString(tokenByte);
                if (!String.IsNullOrEmpty(token))
                {
                    RestClient client = new RestClient("https://localhost:44309/products/all");
                    RestRequest request = new RestRequest
                    {
                        Method = Method.GET
                    };
                    request.Parameters.Clear();
                    request.AddHeader("Authorization", $"bearer {token}");

                    IRestResponse response = client.Execute(request);
                    var model = AllProductsResponseModel.FromJson(response.Content);
                    Product = model;

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        RestClient client1 = new RestClient("https://localhost:44309/Sales/totalMembers");
                        RestRequest request1 = new RestRequest
                        {
                            Method = Method.GET
                        };
                        request1.Parameters.Clear();
                        request1.AddHeader("Authorization", $"bearer {token}");
                        IRestResponse response1 = client1.Execute(request1);
                        if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            TotalMembers = response1.Content;
                        }

                        RestClient client2 = new RestClient("https://localhost:44309/Sales/totalPrice");
                        RestRequest request2 = new RestRequest
                        {
                            Method = Method.GET
                        };
                        request2.Parameters.Clear();
                        request2.AddHeader("Authorization", $"bearer {token}");
                        IRestResponse response2 = client2.Execute(request2);
                        if (response2.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            TotalCash = response2.Content;
                        }

                        RestClient client3 = new RestClient("https://localhost:44309/Sales/allOrders");
                        RestRequest request3 = new RestRequest
                        {
                            Method = Method.GET
                        };
                        request3.Parameters.Clear();
                        request3.AddHeader("Authorization", $"bearer {token}");
                        IRestResponse response3 = client3.Execute(request3);
                        if (response3.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            TotalOrders = response3.Content;
                        }

                        RestClient client4 = new RestClient("https://localhost:44309/Sales/allSoldProducts");
                        RestRequest request4 = new RestRequest
                        {
                            Method = Method.GET
                        };
                        request4.Parameters.Clear();
                        request4.AddHeader("Authorization", $"bearer {token}");
                        IRestResponse response4 = client4.Execute(request3);
                        if (response4.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            TotalSoldProducts = response4.Content;
                        }

                        return Page();
                    }
                    else
                    {
                        return RedirectToPage("/error");
                    }
                }
                return RedirectToPage("/index");
            }
            catch (Exception)
            {
                return RedirectToPage("/error");
            }
        }
    }
}
