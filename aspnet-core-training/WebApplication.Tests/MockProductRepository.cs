using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Infrastructure;
using WebApplication.Models;

namespace WebApplication.Tests
{
    public class MockProductRepository : IProductRepository
    {
        private IList<Product> _products = null;

        public void Setup(IList<Product> products)
        {
            _products = products;
        }

        public IList<Product> Get()
        {
            return _products;
        }
    }
}
