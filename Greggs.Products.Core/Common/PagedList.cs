using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Greggs.Products.Core.Common
{
    public class PagedList<T>
    {
        private readonly int _page;
        private readonly int _size;
        public int Count { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public IQueryable<T> Items { get; set; }

        public PagedList(int page, int size)
        {
            _page = page;
            _size = size;
        }

        public PagedList<T> Execute()
        {
            Items = Items.Skip((_page - 1) * _size)
                     .Take(_size);
            return this;
        }

        public PagedList<TEntity> Project<TEntity>(Expression<Func<T, TEntity>> mapping)
        {
            return new PagedList<TEntity>(_page, _size)
            {
                Items = Items.Select(mapping),
                Count = Count,
                Page= _page,
                Size= _size
            };
        }

    }

}
