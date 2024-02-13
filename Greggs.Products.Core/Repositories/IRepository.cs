using Greggs.Products.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Greggs.Products.Core.Repositories
{
    public interface IRepository<TEntity>  where TEntity : class
    {

        Task<PagedList<TEntity>> GetPagedAsync(int page, int size);

        Task<PagedList<TEntity>> GetPagedAsync(Expression<Func<TEntity, bool>> predicate, int page, int size);

        Task<int> GetDataCountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
