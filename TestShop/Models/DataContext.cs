using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestShop.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        static DataContext()
        {
            Database.SetInitializer<DataContext>(new StoreDbInitializer());
        }
        public DataContext()
            : base("ShopEntities")
        {
        }

        public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<DataContext>
        {
            protected override void Seed(DataContext db)
            {
                db.Categories.Add(new Category {
                    Name = "Смартфоны", Products = new List<Product> {
                        new Product { Name = "Samsung Galaxy A7", Price = 600, Rating = 1, Photo = "/Photo/30042525m.jpg" },
                        new Product { Name = "Huawei P20 Lite", Price = 660, Rating = 3, Photo = "/Photo/30042521m.jpg" },
                        new Product { Name = "Apple iPhone XS Max", Price = 900, Rating = 5, Photo = "/Photo/30040019m.jpg" }
                    }
                });
                db.Categories.Add(new Category {
                    Name = "Компьютеры", Products = new List<Product> {
                        new Product { Name = "Apple MacBook 12 Core M3", Price = 1000, Rating = 3, Photo = "/Photo/30028509m.jpg" },
                        new Product { Name = "Irbis NB211", Price = 630, Rating = 2, Photo = "/Photo/30028509m.jpg" }
                    }
                });
                db.Categories.Add(new Category {
                    Name = "Бытовая техника", Products = new List<Product> {
                        new Product { Name = "Робот-пылесос Tefal Smart Force Extreme RG7145RH", Price = 1000, Rating = 4, Photo="/Photo/20059428m.jpg" },
                        new Product { Name = "Робот-пылесос Philips FC8796/01", Price = 680, Rating = 2, Photo="/Photo/20059428m.jpg" },
                        new Product { Name = "Кофеварка гейзерная Rondell RDS-499", Price = 940, Rating = 3, Photo="/Photo/50045503m.jpg" },
                        new Product { Name = "Холодильник Hotpoint-Ariston HF 9201 X RO", Price = 1300, Rating = 5, Photo="/Photo/20032511m.jpg" }
                    }
                });
                db.SaveChanges();
            }
        }
    }
}