using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestShop.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public string Photo { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}