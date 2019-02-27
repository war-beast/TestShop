using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Рейтинг")]
        public int Rating { get; set; }
        [Display(Name = "Фото")]
        public string Photo { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}