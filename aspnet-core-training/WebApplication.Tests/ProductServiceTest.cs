using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Infrastructure;
using WebApplication.Models;
using WebApplication.Services;
using Xunit;

namespace WebApplication.Tests
{
    public class ProductServiceTest
    {
        [Fact]
        public void Get_ReturnNotFound()
        {
            // Arrange
            var mock = new MockProductRepository();
            IProductService svc = new ProductService(mock);

            // Act
            var result = svc.Get();

            // Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void Get_ReturnProducts()
        {
            // Arrange
            var products = new List<Product> {
                    new Product{ ID = 1001, Name = "Product A", Price = 100000  },
                    new Product{ ID = 1002, Name = "Product B", Price = 200000  },
                    new Product{ ID = 1003, Name = "Product C", Price = 300000  },
                };
            var mock = new MockProductRepository();
            mock.Setup(products);
            IProductService svc = new ProductService(mock);

            // Act
            var result = svc.Get();

            // Assert
            Assert.Equal(products.Count, result.Count);
            for (var i = 0; i < products.Count; i++)
            {
                Assert.Equal(products[i].ID, result[i].ID);
                Assert.Equal(products[i].Name, result[i].Name);
                Assert.Equal(products[i].Price, result[i].Price);
            }
        }
    }
}
