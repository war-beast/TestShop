using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestShop.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}