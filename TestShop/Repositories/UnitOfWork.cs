using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using TestShop.Models;

namespace TestShop.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private DataContext db = new DataContext();
        private CategoryRepository _categoryRepository;
        private ProductRepository _productRepository;
        private CustomerRepository _customer;
        private OrderRepository _orderRepository;

        public CategoryRepository Categories
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(db);
                return _categoryRepository;
            }
        }

        public ProductRepository Products
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(db);
                return _productRepository;
            }
        }

        public CustomerRepository Customer
        {
            get
            {
                if (_customer == null)
                    _customer = new CustomerRepository(db);
                return _customer;
            }
        }

        public OrderRepository Order
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(db);
                return _orderRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}