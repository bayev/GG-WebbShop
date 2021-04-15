using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System;
using System.Text;

namespace GG_Webbshop.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public bool ShowMostPopularProducts { get; set; }

        [BindProperty]
        public bool ShowLatestProducts { get; set; }

        [BindProperty]
        public bool ShowSelectedProducts { get; set; }

        [BindProperty]
        public bool ShowRecommendedProducts { get; set; }
        public AllProductsResponseModel[] Product { get; set; }
        public AllProductsResponseModel[] MostPopularProducts { get; set; }
        public AllProductsResponseModel[] LastestArrivals { get; set; }
        public AllProductsResponseModel[] RecommendedProducts { get; set; }
        public AllProductsResponseModel[] HighlightedProducts { get; set; }

        public IndexModel()
        {
        }

        public IActionResult OnGet()
        {

            try
            {
                RestClient client = new RestClient("https://localhost:44309/algorithm/NewestArrivedProducts");
                
                RestRequest request = new RestRequest
                {
                    Method = Method.GET
                };
                request.Parameters.Clear();

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    

                    var arrivals = AllProductsResponseModel.FromJson(response.Content);
                    LastestArrivals = arrivals;
                    ShowLatestProducts = true;

                    RestClient sClient = new RestClient("https://localhost:44309/algorithm/SelectedProducts");
                    RestRequest sRequest = new RestRequest
                    {
                        Method = Method.GET
                    };
                    sRequest.Parameters.Clear();
                    IRestResponse sResponse = sClient.Execute(sRequest);
                    if(sResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var highLighted = AllProductsResponseModel.FromJson(sResponse.Content);
                        HighlightedProducts = highLighted;
                        ShowSelectedProducts = true;
                    }
                    else
                    {
                        ShowSelectedProducts = false;
                    }

                    RestClient client1 = new RestClient("https://localhost:44309/algorithm/MostPopularProducts");

                    RestRequest request1 = new RestRequest
                    {
                        Method = Method.GET
                    };
                    request1.Parameters.Clear();

                    IRestResponse response1 = client1.Execute(request1);
                    if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var model = AllProductsResponseModel.FromJson(response1.Content);
                        MostPopularProducts = model;
                        ShowMostPopularProducts = true;

                        try
                        {
                            string token = null;
                            byte[] tokenByte;
                            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                            token = Encoding.ASCII.GetString(tokenByte);

                            RestClient client2 = new RestClient("https://localhost:44309/algorithm/RecommendedProducts");
                            RestRequest request2 = new RestRequest
                            {
                                Method = Method.GET
                            };

                            if (!String.IsNullOrEmpty(token))
                            {
                                request2.Parameters.Clear();
                                request2.AddHeader("Authorization", $"bearer {token}");
                                IRestResponse response2 = client2.Execute(request2);
                                if (response2.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var recommended = AllProductsResponseModel.FromJson(response2.Content);
                                    RecommendedProducts = recommended;
                                    ShowRecommendedProducts = true;
                                }
                                else if (response2.StatusCode == System.Net.HttpStatusCode.BadRequest)
                                {
                                    ShowRecommendedProducts = false;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            ShowRecommendedProducts = false;
                            return Page();
                        } 
                    }
                    else if (response1.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ShowLatestProducts = false;

                    }
                    return Page();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ShowMostPopularProducts = false;
                    return Page();
                }

            }
            catch (Exception)
            {
                return RedirectToPage("/error");
            }
            return Page();
        }

    }
}
