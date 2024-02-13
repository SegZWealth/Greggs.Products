using Greggs.Products.Core.DTOs;
using Greggs.Products.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greggs.Products.Core.Repositories
{
    public interface IExchangeRateRepository : IRepository<ExchangeRate>
    {
        Task<ExchangeRateDTO> GetExchangeRateByCurrencyCode(string CurrencyCode);  
    }
}
