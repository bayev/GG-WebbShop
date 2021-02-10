using System;
using System.Collections.Generic;
using System.Text;

namespace DbPrototype.Models.Entities
{
    public class Product
    {  
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public DateTime CreateDate { get; set; }
        public int Stock { get; set; }
        public string Size { get; set; }
        public string Brand { get; set; }

        public IList<ProductCategory> ProductCategories { get; set; }
        public IList<CartToProduct> CartToProducts { get; set; }
    }
}
