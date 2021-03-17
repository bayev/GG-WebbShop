using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GG_Webbshop.Models.ResponseModel
{
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string BillingAddress { get; set; }
        public string DefaultShippingAddress { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
    }
}
