using SampleDI.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Service.Model
{
    public class GetProductResponse
    {
        public bool Success { get; set; }

        public string Exception { get; set; }

        public Product Product { get; set; }
    }
}
