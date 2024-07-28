
using ERestaurant.Domain.Domain;
using EShop.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Domain;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection.Emit;

namespace ERestaurant.Repository
{
    public class ApplicationDbContext : IdentityDbContext<CostumerUser>
    {
        
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryPerson> DeliveryPeople { get; set; }
        public DbSet<ItemInOrder> ItemsInOrder { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Restaurant.Domain.Domain.Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ItemInShoppingCart> ItemsInShoppingCart { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
            builder.Entity<CostumerUser>(entity =>
            {
                entity.Property(e => e.DateRegistration)
                      .HasColumnName("DateRegistration");

                entity.Property(e => e.Phone)
                      .HasColumnName("Phone");
            });

            builder.Entity<Menu>()
                   .HasOne(m => m.Restaurant)
                   .WithMany(r => r.Menues)
                   .HasForeignKey(m => m.RestaurantId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Order>()
            .HasMany(o => o.Deliveries)
            .WithOne(d => d.Order)
            .HasForeignKey(d => d.OrderId);

        }
  

        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EcommerceDb;Trusted_Connection=True;MultipleActiveResultSets=true");

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
    }
}
