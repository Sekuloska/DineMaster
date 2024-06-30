
using ERestaurant.Domain.Domain;
using EShop.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EShop.Domain;
using System.Collections.Generic;
using ERestaurant.Domain;
using Restaurant.Domain.Domain;
using System.Net.Mail;

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
        }
    }
}
