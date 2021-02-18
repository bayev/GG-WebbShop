
using System;
using System.Collections.Generic;
using System.Text;

namespace GG_Webbshop
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<CartToProduct> CartToProducts { get; set; }

    }
}
