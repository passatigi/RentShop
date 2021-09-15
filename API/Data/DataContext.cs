using API.Entities;
using API.Helpers;
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
        public DbSet<RealProduct> RealProducts   { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Feature>()
                .HasOne(f => f.Category)
                .WithMany(c => c.Features)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ProductFeature>()
                .HasKey(k => new {k.ProductId, k.FeatureId});

            builder.Entity<ProductFeature>()
                .HasOne(pf => pf.Product)
                .WithMany(p => p.ProductFeatures)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderProduct>()
                .HasKey(k => new {k.RealProductId, k.OrderId});

            builder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(s => s.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderProduct>()
                .HasOne(op => op.RealProduct)
                .WithMany(rp => rp.OrderProducts)
                .HasForeignKey(s => s.RealProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Order>()
                .HasOne(o => o.Deliveryman)
                .WithMany(d => d.DeliverymanOrders)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .HasOne(o => o.DeliverymanReturn)
                .WithMany(d => d.DeliverymanReturnOrders)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.Entity<Order>()
                .HasOne(u => u.ShippedAddress)
                .WithMany(p => p.ShippedOrders)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .HasOne(u => u.ReturnAddress)
                .WithMany(p => p.ReturnOrders)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DeliverymanSchedule>()
                .HasOne(s => s.Deliveryman)
                .WithMany(m => m.DeliverymanShedules)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyUtcDateTimeConverter();
        }
    }
}