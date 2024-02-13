using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.Utils;
using Greggs.Products.Core.Common;
using Greggs.Products.Core.DTOs;
using Greggs.Products.Core.Models;
using Greggs.Products.Core.Services;
using Greggs.Products.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Greggs.Products.UnitTests.ControllerTests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock = new();
        private readonly ProductController _productController;

        public ProductControllerTests()
        {
            _productController = new ProductController(_productServiceMock.Object);
        }

        [Fact]
        public async Task GetProduct_ReturnListOfProductObjects()
        {
            // Arrange
            List<ProductDTO> testProducts = new()
            {
                new ProductDTO{ ProductId=1, Name= "Mexican Baguette", PriceInPounds= 2.1m, PriceInEuros= 2.33m },
                new ProductDTO {ProductId=2, Name= "Bacon Sandwich", PriceInPounds= 1.95m, PriceInEuros= 2.16m  }
            };
            _productServiceMock
            .Setup(x => x.GetAllProducts(1, 2))
                .ReturnsAsync(new PagedList<ProductDTO>(1, 2)
                {
                    Items = testProducts.AsQueryable(),
                    Count = 2
                });
            // Act
            var apiResult = await _productController.Get(1, 2);
            // Assert
            Assert.NotNull(apiResult);
            Assert.Equal(HttpHelpers.GetStatusCodeValue(HttpStatusCode.OK), apiResult.Code);
            Assert.Equal(2, apiResult.Object.Count);
        }

    }
}
