using Greggs.Products.Core.Common;
using Greggs.Products.Core.Models;
using Greggs.Products.Core.Repositories;
using Greggs.Products.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greggs.Products.Infrastructure.Utils
{
    public class UserSeed
    {
        public static void SeedDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {

                scope.ServiceProvider.GetRequiredService<ProductContext>().Database.Migrate();
                var _context = scope.ServiceProvider.GetRequiredService<ProductContext>();
                CreateProducts(_context);
                CreateExchangeRate(_context);
            }

            Console.WriteLine("Done seeding database.");
        }

        static void CreateProducts(ProductContext _context)
        {
            if (_context.Products.ToList().Count != 0)
                return;
            var products = new List<Product>()
            {
               new() { Name = "Sausage Roll", PriceInPounds = 1m, CreatedBy = "system" },
               new() { Name = "Vegan Sausage Roll", PriceInPounds = 1.1m , CreatedBy = "system"},
               new() { Name = "Steak Bake", PriceInPounds = 1.2m , CreatedBy = "system"},
               new() { Name = "Yum Yum", PriceInPounds = 0.7m , CreatedBy = "system"},
               new() { Name = "Pink Jammie", PriceInPounds = 0.5m , CreatedBy = "system"},
               new() { Name = "Mexican Baguette", PriceInPounds = 2.1m , CreatedBy = "system"},
               new() { Name = "Bacon Sandwich", PriceInPounds = 1.95m , CreatedBy = "system"},
               new() { Name = "Coca Cola", PriceInPounds = 1.2m , CreatedBy = "system"}
            };
            _context.AddRange(products);
            _context.SaveChanges();
        }

        static void CreateExchangeRate(ProductContext _context)
        {

            if (_context.ExchangeRates.ToList().Count != 0)
                return;
            _context.Add(new ExchangeRate { Currency = "EURO", CurrencyCode = "EUR", Rate = 1.11m, CreatedBy = "system" });
            _context.SaveChanges();
        }

    }
}
