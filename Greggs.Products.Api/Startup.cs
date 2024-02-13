using Greggs.Products.Core.Common;
using Greggs.Products.Core.Repositories;
using Greggs.Products.Core.Services;
using Greggs.Products.Infrastructure.Repositories;
using Greggs.Products.Infrastructure.Services;
using Greggs.Products.Infrastructure.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Greggs.Products.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ProductContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString(CoreConstants.ConnectionStringName))
              , ServiceLifetime.Transient);

        services.AddTransient<DbContext>((_) =>
        {
            var connStr = Configuration.GetConnectionString(CoreConstants.ConnectionStringName);
            return new ProductContext(new DbContextOptionsBuilder<ProductContext>()
                                     .UseSqlServer(connStr)
                                     .Options);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();

        services.AddControllers();

        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            UserSeed.SeedDatabase(app);
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Greggs Products API V1"); });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}