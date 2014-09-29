using SampleDI.Cache;
using SampleDI.Configuration;
using SampleDI.Context;
using SampleDI.Domain.Repository;
using SampleDI.Email;
using SampleDI.File;
using SampleDI.Logging;
using SampleDI.Service;
using SampleDI.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleDI
{
    class Program
    {
        static void Main(string[] args)
        {
            IProductRepository productRepository = new ProductRepository();
            ICacheStorage cacheStorage = new SystemRuntimeCacheStorage();
            IConfigurationRepository configurationRepository = new ConfigFileConfigurationRepository();
            IContextService contextService =  new ThreadContextService();
            IFileService fileService = new DefaultFileService();
            ILoggingService loggingService = new Log4NetLoggingService(configurationRepository, contextService);
            // loggingService = new ConsoleLoggingService();
            IEmailService emailService = new EmailService();
            var productService = new ProductService(productRepository, cacheStorage, configurationRepository, loggingService);

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
