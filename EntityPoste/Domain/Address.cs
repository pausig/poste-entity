﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityPoste.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public override string ToString() => $"{Street}, {City}, {Country}";
    }
}
