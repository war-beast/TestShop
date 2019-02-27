using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Рейтинг")]
        [Required]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "Фото")]
        public string Photo { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}