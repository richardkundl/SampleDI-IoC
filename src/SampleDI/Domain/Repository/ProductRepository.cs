using SampleDI.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Domain.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Product FindBy(int Id)
        {
            return All().FirstOrDefault(p => p.Id == Id);
        }

        public IEnumerable<Product> All()
        {
            return new List<Product>{
                  new Product { Id = 1, Name = "Chair", OnStock = 10 },
                  new Product { Id = 2, Name = "Desk", OnStock = 20},
                  new Product { Id = 3, Name = "Cupboard", OnStock = 15 }
            };
        }
    }
}
