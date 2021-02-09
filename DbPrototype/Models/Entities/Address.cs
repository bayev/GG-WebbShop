using System;
using System.Collections.Generic;
using System.Text;

namespace DbPrototype.Models.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public User User { get; set; }
    }
}
