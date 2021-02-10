using System;
using System.Collections.Generic;
using System.Text;

namespace DbPrototype.Models.Entities
{
    public class CartToProduct
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Timestamp { get; set; }
        public int Amount { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }
        
    }
}
