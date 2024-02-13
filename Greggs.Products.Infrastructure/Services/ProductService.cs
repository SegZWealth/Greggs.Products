﻿using Greggs.Products.Core.Common;
using Greggs.Products.Core.DTOs;
using Greggs.Products.Core.Models;
using Greggs.Products.Core.Repositories;
using Greggs.Products.Core.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Greggs.Products.Infrastructure.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        public ProductService(IUnitOfWork uow) : base(uow)
        {

        }
        public async Task<PagedList<ProductDTO>> GetAllProducts(int pageStart, int pageSize)
        {
            var products = await _uow.Product.GetPagedAsync(pageStart, pageSize);
            var rate = await _uow.ExchangeRate.GetExchangeRateByCurrencyCode(CoreConstants.EUROCURRENCYCODE);
            return products.Execute().Project(product => new ProductDTO { ProductId = product.ProductId, Name = product.Name, PriceInPounds = product.PriceInPounds, PriceInEuros = rate != null ? Math.Round(product.PriceInPounds * rate.Rate, 2) : 0 });
        }
    }
}

