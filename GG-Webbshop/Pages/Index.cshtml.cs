using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System;

namespace GG_Webbshop.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public bool ShowMostPopularProducts { get; set; }
        [BindProperty]
        public bool ShowLatestProducts { get; set; }
        public AllProductsResponseModel[] Product { get; set; }
        public AllProductsResponseModel[] MostPopularProducts { get; set; }
        public AllProductsResponseModel[] LastestArrivals { get; set; }

        public IndexModel()
        {
        }

        public IActionResult OnGet()
        {
            try
            {
                RestClient client = new RestClient("https://localhost:44309/algorithm/MostPopularProducts");
                RestRequest request = new RestRequest
                {
                    Method = Method.GET
                };
                request.Parameters.Clear();

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var model = AllProductsResponseModel.FromJson(response.Content);
                    MostPopularProducts = model;
                    ShowMostPopularProducts = true;

                    RestClient client1 = new RestClient("https://localhost:44309/algorithm/NewestArrivedProducts");
                    RestRequest request1 = new RestRequest
                    {
                        Method = Method.GET
                    };
                    request1.Parameters.Clear();

                    IRestResponse response1 = client1.Execute(request1);
                    if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var arrivals = AllProductsResponseModel.FromJson(response1.Content);
                        LastestArrivals = arrivals;
                        ShowLatestProducts = true;
                    }
                    return Page();
                    //// //// //// //// //// ////NEDAN SKA BYTAS UT
                    //RestClient client = new RestClient("https://localhost:44309/Query/all");
                    //RestRequest request = new RestRequest
                    //{
                    //    Method = Method.GET
                    //};
                    //request.Parameters.Clear();

                    //IRestResponse response = client.Execute(request);

                    //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    //{
                    //var model = AllProductsResponseModel.FromJson(response.Content);

                    //Product = model;

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ShowMostPopularProducts = false;
                    return Page();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ShowLatestProducts = false;
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
