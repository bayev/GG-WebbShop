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
        public string UserID { get; set; }
        public string Role { get; set; }
    }
}
