using System;
using System.Collections.Generic;
using System.Text;

namespace GG_Webbshop
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<ProductCategory> ProductCategories { get; set; }

    }
}
