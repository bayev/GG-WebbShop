using System;
using System.Collections.Generic;
using System.Text;

namespace DbPrototype.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string BillingAddress { get; set; }
        public string DefaultShippingAddress { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public IList<Address> Addresses { get; set; }

        //Will be added later.
        // public int RoleId { get; set; }

    }
}
