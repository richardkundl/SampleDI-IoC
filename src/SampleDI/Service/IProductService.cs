using SampleDI.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Service
{
    public interface IProductService
    {
        GetProductResponse GetProduct(GetProductRequest getProductRequest);
    }
}
