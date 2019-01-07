﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFly.Entities
{
    public class Customers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public object Phone { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string UF { get; set; }
        public object Contact { get; set; }
        public string ServiceDeskLogin { get; set; }
        public string ServiceDeskPassword { get; set; }
        public bool Active { get; set; }
    }
}
