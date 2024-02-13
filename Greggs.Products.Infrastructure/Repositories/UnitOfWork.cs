using Greggs.Products.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greggs.Products.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private volatile bool _isDisposing;
        private readonly ProductContext _context;
        private readonly Dictionary<Type, object> _unitOfWorkCache = new();
        public UnitOfWork(ProductContext context)
        {
            _context = context;
        }
        public IProductRepository Product => GetOrAdd(() => new ProductRepository(_context));

        public IExchangeRateRepository ExchangeRate => GetOrAdd(() => new ExchangeRateRepository(_context));

        public void Complete()
        {
            _context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            using var transaction = _context.Database.BeginTransaction() ;
            try
            {

                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }

        protected T GetOrAdd<T>(Func<T> update) where T : class
        {
            object item;

            var key = typeof(T);

            if (_unitOfWorkCache.TryGetValue(key, out item))
            {
                return (T)item;
            }

            item = update.Invoke();
            _unitOfWorkCache.Add(key, item);

            return (T)item;
        }
        protected void Dispose(bool disposing)
        {
            if (_isDisposing) return;

            _isDisposing = disposing;
            _context.Dispose();
            _isDisposing = false;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
