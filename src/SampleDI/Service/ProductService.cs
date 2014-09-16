using SampleDI.Cache;
using SampleDI.Configuration;
using SampleDI.Domain.Entity;
using SampleDI.Domain.Repository;
using SampleDI.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly ICacheStorage _cacheStorage;

        private readonly IConfigurationRepository _configurationRepository;

        public ProductService(IProductRepository productRepository, 
                                ICacheStorage cacheStorage, 
                                IConfigurationRepository configurationRepository)
        {
            if (productRepository == null)
            {
                throw new ArgumentNullException("ProductRepository");
            }

            if (cacheStorage == null)
            {
                throw new ArgumentNullException("CacheStorage");
            }

            if (configurationRepository == null) {
                throw new ArgumentException("Configuration");
            }

            this._productRepository = productRepository;
            this._cacheStorage = cacheStorage;
            this._configurationRepository = configurationRepository;
        }

        public GetProductResponse GetProduct(GetProductRequest getProductRequest)
        {
            var response = new GetProductResponse();

            try
            {
                var cacheKey = "GetProductById";
                var returnDefault = this._configurationRepository.GetConfigurationValue<bool>("ReturnDefaultProduct");
                response.Product = this._cacheStorage.Get<Product>(cacheKey);

                if (response.Product == null)
                {
                    response.Product = this._productRepository.FindBy(getProductRequest.Id);
                    if (response.Product != null) { 
                        this._cacheStorage.Set(cacheKey, response.Product, DateTime.Now.AddMinutes(5), TimeSpan.Zero);
                    }
                }       

                if (returnDefault && response.Product == null) {
                    response.Product = BuildDefaultProduct();
                }
                else if (!returnDefault && response.Product == null) {
                    response.Exception = "No such product.";
                }

                if (response.Product != null)
                {
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Exception = ex.Message;
            }

            return response;
        }

        private Product BuildDefaultProduct() {
            return new Product {
                Id = this._configurationRepository.GetConfigurationValue<int>("DefaultProductId"),
                Name = this._configurationRepository.GetConfigurationValue<string>("DefaultProductName"),
                OnStock = this._configurationRepository.GetConfigurationValue<int>("DefaultProductQuantity"),
            };
        }
    }
}
