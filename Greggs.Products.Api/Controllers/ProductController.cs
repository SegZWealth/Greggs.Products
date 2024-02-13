using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greggs.Products.Core.Common;
using Greggs.Products.Core.DTOs;
using Greggs.Products.Core.Services;
using Greggs.Products.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Greggs.Products.Api.Controllers;


public class ProductController : BaseController
{
    private readonly IProductService _ProductSrvc;

    public ProductController(IProductService ProductSrvc)
    {
        _ProductSrvc = ProductSrvc;
    }

    [HttpGet]
    [Route("{pageStart}/{pageSize}")]
    public async Task<IServiceResponse<PagedList<ProductDTO>>> Get(int pageStart = 1, int pageSize = 5)
    {

        return await HandleApiOperationAsync(async () =>
        {
            var products = await _ProductSrvc.GetAllProducts(pageStart, pageSize);
            return new ServiceResponse<PagedList<ProductDTO>>
            {
                Object = products
            };
        });
    }
}