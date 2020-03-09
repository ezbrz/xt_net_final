﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Mercato.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public int Producer { get; set; }
        public string Characteristics { get; set; }
        public int Country { get; set; }
        public byte[] Photo { get; set; }
        public decimal Price { get; set; }
    }
}
