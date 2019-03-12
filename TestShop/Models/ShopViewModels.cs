using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestShop.Models
{
    public class OrderState : List<String>
    {
        private readonly int AvailableStateCount = 3;
        public static List<String> StateList { get; set; }

        public OrderState()
        {
            StateList = new List<string>(AvailableStateCount);
            StateList.Add("В обработке");
            StateList.Add("Выставлен счёт");
            StateList.Add("Счёт оплачен");
            StateList.Add("Товар отправлен покупателю");
            StateList.Add("Товар доставлен по адресу");
        }
    }

    public class Parametr
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ShopingCardItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }

    public class ShopingCardViewModel
    {
        public ICollection<ShopingCardItem> Items { get; set; }
        public string Adress { get; set; }
    }

    public class FilterViewModels
    {
        public List<Parametr> Categories { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int Sort { get; set; }
    }

    public class SortType
    {
        private List<Parametr> SortList { get; set; }
        public SortType()
        {
            SortList = new List<Parametr> {
                new Parametr { Id = 0, Name = "Умолчанию" },
                new Parametr { Id = 1, Name= "Цене" },
                new Parametr { Id = 2, Name = "Рейтингу" } };
        }

        public List<Parametr> GetSortTypes()
        {
            return SortList;
        }

        public Parametr Get(int Id)
        {
            return SortList.FirstOrDefault(st => st.Id == Id);
        }
    }
}