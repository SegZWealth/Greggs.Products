using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greggs.Products.Core.DTOs
{
    public class ProductDTO
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public decimal PriceInPounds { get; set; }
        public decimal PriceInEuros { get; set; } 
    }
}
