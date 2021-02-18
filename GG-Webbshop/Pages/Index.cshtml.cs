using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GG_Webbshop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GG_Webbshop.AppDbContext _context;

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IEnumerable<Product> Products { get; set; }

        public void OnGet()
        {
            //Seed();
            Products = _context.Products;
        }
        public void Seed()
        {
            List<Product> seedList = new List<Product>();

            Product product = new Product();
            product.Name = "Morris Pullover";
            product.Price = 599;
            product.Weight = 400;
            product.Description = "En stilren pullover från Morris som säkrar vardagsstilen en kall och mulen höstdag eller en sensommarkväll.";
            product.Image = "MorrisSweater.jpg";
            product.Category = "Sweater";
            product.CreateDate = DateTime.Now;
            product.Stock = 10;
            product.Size = "M";
            product.Brand = "Morris";

            Product product1 = new Product();
            product1.Name = "Calvin Klein tröja";
            product1.Price = 799;
            product1.Weight = 425;
            product1.Description = "Grå sweater från CK, en stilren tröja som funkar både i hemmet och på stan.";
            product1.Image = "CkSweater.jpg";
            product1.Category = "Sweater";
            product1.CreateDate = DateTime.Now;
            product1.Stock = 10;
            product1.Size = "L";
            product1.Brand = "Calvin Klein";

            Product product2 = new Product();
            product2.Name = "Ralph Lauren skjorta";
            product2.Price = 699;
            product2.Weight = 355;
            product2.Description = "Stilren, svart skjorta från RL. Bär till jeans eller kostym, inga ursäkter för att inte bära denna sköna skjorta.";
            product2.Image = "RalphLaurenShirt.jpg";
            product2.Category = "Shirt";
            product2.CreateDate = DateTime.Now;
            product2.Stock = 10;
            product2.Size = "S";
            product2.Brand = "Ralph Lauren";

            Product product3 = new Product();
            product3.Name = "Philippa K kostymbyxor";
            product3.Price = 1499;
            product3.Weight = 445;
            product3.Description = "Ett par stilrena kostymbyxor från Philippa K är du säker på att du håller stilen, passar till vardags så väl som till fest.";
            product3.Image = "PhilippaKTrousers.jpg";
            product3.Category = "Pants";
            product3.CreateDate = DateTime.Now;
            product3.Stock = 10;
            product3.Size = "M";
            product3.Brand = "Philippa K";

            Product product5 = new Product();
            product5.Name = "Rains ryggsäck";
            product5.Price = 899;
            product5.Weight = 500;
            product5.Description = "Men en ryggsäck från Rains får du med dig laptop, ipad, och andra atteraljer utan att tumma på stilen. Gjord i vattentätt material så din elektronik inte blir förstörd om det skulle regna.";
            product5.Image = "RainsBackpack.jpg";
            product5.Category = "Accessories";
            product5.CreateDate = DateTime.Now;
            product5.Stock = 10;
            product5.Size = null;
            product5.Brand = "Rain";

            Product product6 = new Product();
            product6.Name = "Tiger of Sweden Blazer";
            product6.Price = 2499;
            product6.Weight = 500;
            product6.Description = "Med en blazer från Tiger of Sweden kan du vara säker på att du håll stilen både till fest och vardag. Gjord i kvalitetsmaterial med bekväm passform.";
            product6.Image = "TigerBlazer.jpg";
            product6.Category = "Blazer";
            product6.CreateDate = DateTime.Now;
            product6.Stock = 5;
            product6.Size = "M";
            product6.Brand = "Tiger of Sweden";

            Product product7 = new Product();
            product7.Name = "Dolce & Gabbana Chinos";
            product7.Price = 1199;
            product7.Weight = 350;
            product7.Description = "Säkra stilen med grå-rutiga chinos från [brand] med skön passform och kvalitetsmaterial.";
            product7.Image = "DolceNgabbanaChinos.jpg";
            product7.Category = "Pants";
            product7.CreateDate = DateTime.Now;
            product7.Stock = 5;
            product7.Size = "L";
            product7.Brand = "Dolce & Gabbana";

            Product product8 = new Product();
            product8.Name = "Plånbok från Hugo Boss";
            product8.Price = 699;
            product8.Weight = 50;
            product8.Description = "Med en plånbok från Hugo Boss kommer du inte dra dig från att vara en gentleman och betala notan. Allt för att visa upp denna stilrena, praktiska plånbok med plats för alla dina kort och kontanter.";
            product8.Image = "HugoBossWallet.jpg";
            product8.Category = "Accessories";
            product8.CreateDate = DateTime.Now;
            product8.Stock = 10;
            product8.Size = null;
            product8.Brand = "Hugo Boss";

            Product product9 = new Product();
            product9.Name = "Manschettknappar från Skultuna";
            product9.Price = 499;
            product9.Weight = 50;
            product9.Description = "Snygga till din vardag med manschettknappar från Skultuna. En snygg, stilren detalj som passar till de flesta skortor och tillfällen. Tillverkade i 18k guldpläterad mässing prydd med Tre kronor.";
            product9.Image = "Manschett.jpg";
            product9.Category = "Accessories";
            product9.CreateDate = DateTime.Now;
            product9.Stock = 10;
            product9.Size = null;
            product9.Brand = "Skultuna";

            seedList.Add(product);
            seedList.Add(product1);
            seedList.Add(product2);
            seedList.Add(product3);
            seedList.Add(product5);
            seedList.Add(product6);
            seedList.Add(product7);
            seedList.Add(product8);
            seedList.Add(product9);

            foreach (var item in seedList)
            {
                _context.Add(item);
            }

            _context.SaveChanges();
        }
    }
}
