using System;
using System.Collections.Generic;
using System.Text;

namespace DbPrototype.Models.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int TimeStamp { get; set; }

        public User User { get; set; }
        public IList<Product> Products { get; set; }





    }
}
