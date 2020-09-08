using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        // Labo #7 ---------------------------- ↓
        private readonly ILogger _logger;

        public ProductRepository(ILogger<ProductRepository> logger)
        {
            _logger = logger;
        }
        // Labo #7 ---------------------------- ↑

        public IList<Product> Get()
        {
            // Labo #7 ---------------------------- ↓
            _logger.LogInformation("Example log message.");
            // Labo #7 ---------------------------- ↑

            var json = File.ReadAllText("Products.json");
            return JsonConvert.DeserializeObject<List<Product>>(json);
        }
    }
}
