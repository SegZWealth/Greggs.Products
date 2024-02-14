using Greggs.Products.Core.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Greggs.Products.Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TContext>
    where TEntity : class
    where TContext : DbContext
    {
        protected readonly TContext Context;
        private readonly DbSet<TEntity> _dbSet;

        protected Repository(TContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        public TEntity Get(long entityId)
        {
            return _dbSet.Find(entityId);
        }
        public Task<PagedList<TEntity>> GetPagedAsync(int page, int size)
        {
            return GetPagedAsync(item => true, page, size);
        }

        public async Task<PagedList<TEntity>> GetPagedAsync(Expression<Func<TEntity, bool>> predicate, int page, int size)
        {
            var count = await GetDataCountAsync(predicate);

            if (size <= 0)
                size = count;
            var items = _dbSet.Where(predicate)
                              .AsNoTracking();

            return new PagedList<TEntity>(page, size)
            {
                Items = items,
                Count = count
            };
        }

        public Task<int> GetDataCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.CountAsync(predicate);
        }

        public void Add(TEntity entity) 
        {
            _dbSet.Add(entity);
        }
    }
}
