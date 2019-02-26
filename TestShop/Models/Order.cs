using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}