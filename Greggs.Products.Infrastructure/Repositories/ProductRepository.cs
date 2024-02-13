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
    public class ProductRepository : Repository<Product, ProductContext>, IProductRepository
    {
        public ProductRepository(ProductContext context) : base(context)
        {

        }

        public Task<List<ProductDTO>> GetAllProducts()
        {
            var products =
           from product in Context.Products



           select new ProductDTO
           {
               ProductId = product.ProductId,
               Name = product.Name,
               PriceInPounds = product.PriceInPounds
           };

            return products.AsNoTracking().ToListAsync();
        }
    }
}
