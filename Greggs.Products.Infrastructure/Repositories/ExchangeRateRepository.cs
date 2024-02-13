using Greggs.Products.Core.DTOs;
using Greggs.Products.Core.Models;
using Greggs.Products.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greggs.Products.Infrastructure.Repositories
{
    public class ExchangeRateRepository : Repository<ExchangeRate, ProductContext>, IExchangeRateRepository
    {
        public ExchangeRateRepository(ProductContext context) : base(context)
        {

        }

        public Task<ExchangeRateDTO> GetExchangeRateByCurrencyCode(string CurrencyCode)
        {
            var rates =
          from rate in Context.ExchangeRates where rate.CurrencyCode == CurrencyCode



          select new ExchangeRateDTO
          {
              ExchangeRateId = rate.ExchangeRateId,
              Currency = rate.Currency,
              CurrencyCode = rate.CurrencyCode,
              Rate= rate.Rate
          };

            return rates.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
