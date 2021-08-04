using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Seed;
using API.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           
            // Product clsInfo = new Product(){ Name = "img", Vendor = "Category", Description = "blabla", CategoryId = 9 };
	        // ProductImg productImg1 = new ProductImg() {Link = "https://images.unsplash.com/photo-1580087256394-dc596e1c8f4f?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=634&q=80"
            
            // };
            // ProductImg productImg2 = new ProductImg() {Link = "https://images.unsplash.com/photo-1580087256394-dc596e1c8f4f?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=634&q=80"
            
            // };
            // clsInfo.ProductImgs = new List<ProductImg>{ productImg1, productImg2};

            // ProductFeature productFeature1 = new ProductFeature() { FeatureId = 1, ProductId = 1, Value = "haha" };
            // ProductFeature productFeature2 = new ProductFeature() { FeatureId = 3, ProductId = 4, Value = "haha" };

            // clsInfo.ProductFeatures = new List<ProductFeature> { productFeature1, productFeature2};

		    // String jsonOutput = JsonConvert.SerializeObject(clsInfo);
		    // Console.WriteLine(jsonOutput);

            
            
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try {
                var context = services.GetRequiredService<DataContext>();
                // var userManager = services.GetRequiredService<UserManager<AppUser>>();
                // var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                await context.Database.MigrateAsync();
                Seeder seeder = new Seeder();
                await seeder.SeedData(context);
            }
            catch(Exception ex){
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured during migration");
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
