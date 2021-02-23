using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GG_Webbshop.Models
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public string Expires { get; set; }
    }

    public class LoginModel
    {
        public string user { get; set; }
        public string password { get; set; }
    }
}
