using Greggs.Products.Core.Common;
using Greggs.Products.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greggs.Products.Core.Services
{
    public interface IProductService
    {
        Task<PagedList<ProductDTO>> GetAllProducts(int pageStart, int pageSize);
        Task AddProduct(ProductDTO productDTO);

        Task UpdateProduct(long ProductId, ProductDTO productDTO);
    }
}
