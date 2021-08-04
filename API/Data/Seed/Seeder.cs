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
            //await SeedCategories(dataContext);

            var categories = await SeedEntities<Category>(dataContext, "CategorySeedData.json");
            foreach(var category in categories)
            {
                if(category.ParentCategoryId != null)
                    category.ParentCategory = categories.FirstOrDefault(c => c.Id == category.ParentCategoryId);
            }


            var features = await SeedEntities<Feature>(dataContext, "FeatureSeedData.json");
            var products = await SeedEntities<Product>(dataContext, "ProductSeedData.json");


            await dataContext.SaveChangesAsync();
        }

        
        // public  async Task SeedCategories(DataContext dataContext)
        // {
        //     if(await dataContext.Categories.AnyAsync()) return;

        //     var categoryData = await File.ReadAllTextAsync(folderPath + "CategorySeedData.json");
        //     var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);
        //     if(categories == null) return;

            

        //     foreach(var category in categories){
        //         dataContext.Categories.Add(category);
        //     }

        //     //await dataContext.SaveChangesAsync();
        // }
        
        private async Task<List<T>> GetObjectsFromJson<T>(string fileName)
        {
            var data = await File.ReadAllTextAsync(folderPath + fileName);
            return JsonSerializer.Deserialize<List<T>>(data);
        }

        public async Task<List<T>> SeedEntities<T>(DataContext dataContext, string fileName) where T : class
        {
            var dbSet = dataContext.Set<T>();

            var objects = new List<T>();
            if(await dbSet.AnyAsync()) return objects;

            objects = await GetObjectsFromJson<T>(fileName);
            if(objects.Count() == 0) return objects;

            foreach(var product in objects){
                dbSet.Add(product);
            }

            return objects;
        }

        

        // public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager){
        //     if(await userManager.Users.AnyAsync()) return;

        //     var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        //     var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
        //     if(users == null) return;

        //     var roles = new List<AppRole>
        //     {
        //         new AppRole{ Name = "Member"},
        //         new AppRole{ Name = "Admin"},
        //         new AppRole{ Name = "Moderator"},
        //     };

        //     foreach(var role in roles){
        //         await roleManager.CreateAsync(role);
        //     }

        //     foreach(var user in users)
        //     {
        //         user.UserName = user.UserName.ToLower();
        //         await userManager.CreateAsync(user, "password");
        //         await userManager.AddToRoleAsync(user, "Member");
        //     }

        //     var admin = new AppUser
        //     {
        //         UserName = "admin"
        //     };

        //     await userManager.CreateAsync(admin, "password");
        //     await userManager.AddToRolesAsync(admin,new[] {"Admin", "Moderator"});
        // }
    }
}