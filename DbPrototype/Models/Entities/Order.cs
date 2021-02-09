using System;
using System.Collections.Generic;
using System.Text;

namespace DbPrototype.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //public int AddressId { get; set; }
        public decimal Amount { get; set; }
        public string OrderEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentMethod { get; set; }

        public IList<OrderDetail> OrderDetails { get; set; }
        //public Address Addresses { get; set; }

        public User User { get; set; }

    }
}
