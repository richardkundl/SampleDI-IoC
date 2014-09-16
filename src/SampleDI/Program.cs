using SampleDI.Cache;
using SampleDI.Configuration;
using SampleDI.Domain.Repository;
using SampleDI.Service;
using SampleDI.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI
{
    class Program
    {
        static void Main(string[] args)
        {
            var productService = new ProductService(new ProductRepository(), new SystemRuntimeCacheStorage(), new ConfigFileConfigurationRepository());
            var response = productService.GetProduct(new GetProductRequest { Id = 12 });
            if (response.Success)
            {
                Console.WriteLine(string.Concat("Product name: ", response.Product.Name));
            }
            else
            {
                Console.WriteLine(response.Exception);
            }

            Console.ReadKey();
        }
    }
}
