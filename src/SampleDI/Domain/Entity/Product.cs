using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Domain.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OnStock { get; set; }
    }
}
