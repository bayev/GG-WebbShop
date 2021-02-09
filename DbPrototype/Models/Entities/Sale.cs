using System;
using System.Collections.Generic;
using System.Text;

namespace DbPrototype.Models.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AmountSold { get; set; }
        public DateTime LastSold { get; set; }
        public int Discount { get; set; }
        public Product Product { get; set; }

    }
}
