using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>,
        AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
        IdentityUserToken<int>>
    {
         public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Address> Addresses  { get; set; }
        public DbSet<Category> Categories  { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<DeliverymanSchedule> DeliverymanSchedules  { get; set; }
        public DbSet<Message> Messages  { get; set; }
        public DbSet<Order> Orders  { get; set; }
        public DbSet<OrderProduct> OrderProducts  { get; set; }
        public DbSet<Product> Products  { get; set; }
        public DbSet<ProductFeature> ProductFeatures   { get; set; }
        public DbSet<ProductImg> ProductImgs   { get; set; }
        public DbSet<RealProduct> realProducts   { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            

            builder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.ChildCategories)
            .OnDelete(DeleteBehavior.NoAction);

        }
    }
}