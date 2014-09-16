using SampleDI.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Domain.Repository
{
    public interface IProductRepository
    {
        Product FindBy(int Id);
    }
}
