using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestShop.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}