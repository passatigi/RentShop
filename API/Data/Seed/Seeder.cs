using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Seed
{
    public class Seeder
    {
        readonly string folderPath = "Data/Seed/";

        public async Task SeedData(DataContext dataContext)
        {
            var categories = await SeedEntities<Category>(dataContext, "CategorySeedData.json");

            var features = await SeedEntities<Feature>(dataContext, "FeatureSeedData.json");
            var products = await SeedEntities<Product>(dataContext, "ProductSeedData.json");
            var orders = await SeedEntities<Order>(dataContext, "OrdersSeedData.json");
        }

        
        private async Task<List<T>> GetObjectsFromJson<T>(string fileName)
        {
            var data = await File.ReadAllTextAsync(folderPath + fileName);
            return JsonSerializer.Deserialize<List<T>>(data);
        }

        public async Task<List<T>> SeedEntities<T>(DataContext dataContext, string fileName) where T : class
        {
            var dbSet = dataContext.Set<T>();

            var objects = new List<T>();
            if(await dbSet.AnyAsync()) 
            {
                return await dbSet.ToListAsync();
            }

            objects = await GetObjectsFromJson<T>(fileName);
            if(objects.Count() == 0) return objects;

            foreach(var product in objects){
                dbSet.Add(product);
            }

            await dataContext.SaveChangesAsync();

            return objects;
        }

        public async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager){
            if(await userManager.Users.AnyAsync()) return;

            
            var users = await GetObjectsFromJson<AppUser>("AppUsersSeedData.json");
            if(users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{ Name = "Customer"},
                new AppRole{ Name = "Admin"},
                new AppRole{ Name = "Deliveryman"},
            };

            foreach(var role in roles){
                await roleManager.CreateAsync(role);
            }

            foreach(var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "password");
                await userManager.AddToRoleAsync(user, "Customer");
            }

            var admin = new AppUser
            {
                UserName = "admin",
                FullName = "admin",
                Email = "admin@m",
                PhoneNumber = "+374 29 855 99 99"
            };
            var deliveryman = new AppUser
            {
                UserName = "deliveryman",
                FullName = "deliveryman",
                Email = "delivery@m",
                PhoneNumber = "+374 29 222 99 99"
            };

            await userManager.CreateAsync(admin, "password");
            await userManager.AddToRolesAsync(admin,new[] {"Admin"});

            await userManager.CreateAsync(deliveryman, "password");
            await userManager.AddToRolesAsync(deliveryman,new[] {"Deliveryman"});
        }
    }
}