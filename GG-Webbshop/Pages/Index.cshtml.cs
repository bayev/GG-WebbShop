using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;

namespace GG_Webbshop.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;

        public AllProductsResponseModel[] Product { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            try
            {

                RestClient client = new RestClient("https://localhost:44309/Query/all");
                RestRequest request = new RestRequest
                {
                    Method = Method.GET
                };
                request.Parameters.Clear();

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var model = AllProductsResponseModel.FromJson(response.Content);

                    Product = model;
                    return Page();
                }
                else
                {
                    return RedirectToPage("/error");
                }
            }
            catch (Exception)
            {
                return RedirectToPage("/error");
            }
        }

    }
}
