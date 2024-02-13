using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greggs.Products.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; } 
        IExchangeRateRepository ExchangeRate { get; }
        void Complete();
        Task CompleteAsync();
    }
}
