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
        public static async Task SeedData(DataContext dataContext)
        {
            await SeedCategories(dataContext);
        }
        public static async Task SeedCategories(DataContext dataContext)
        {
            if(await dataContext.Users.AnyAsync()) return;

            var categoryData = await File.ReadAllTextAsync("Data/Seed/CategorySeedData.json");
            var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);
            if(categories == null) return;

            foreach(var category in categories){
                if(category.ParentCategoryId != null)
                    category.ParentCategory = categories.FirstOrDefault(c => c.Id == category.ParentCategoryId);
            }

            foreach(var category in categories){
                dataContext.Categories.Add(category);
            }

            await dataContext.SaveChangesAsync();
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