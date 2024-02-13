using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greggs.Products.Core.DTOs
{
    public class ExchangeRateDTO
    {
        public long ExchangeRateId { get; set; }
        public string Currency { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Rate { get; set; }
    }
}
