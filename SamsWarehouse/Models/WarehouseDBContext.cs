using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace SamsWarehouse.Models
{
    public class WarehouseDBContext : DbContext
    {
        public WarehouseDBContext(DbContextOptions<WarehouseDBContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)").HasDefaultValue(0);
            });

            modelBuilder.Entity<ShoppingListItem>(entity =>
            {
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<ShoppingList>()
                .HasMany(s => s.ListItems)
                .WithOne()
                .HasForeignKey(li => li.ShoppingListId);

            // Seed products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ItemName = "Granny Smith Apples", Unit = "1kg", UnitPrice = 5.50m },
                new Product { ProductId = 2, ItemName = "Fresh tomatoes", Unit = "500g", UnitPrice = 5.90m },
                new Product { ProductId = 3, ItemName = "Watermelon", Unit = "Whole", UnitPrice = 6.60m },
                new Product { ProductId = 4, ItemName = "Cucumber", Unit = "1 whole", UnitPrice = 1.90m },
                new Product { ProductId = 5, ItemName = "Red potato washed", Unit = "1kg", UnitPrice = 4.00m },
                new Product { ProductId = 6, ItemName = "Red tipped bananas", Unit = "1kg", UnitPrice = 4.90m },
                new Product { ProductId = 7, ItemName = "Red onion", Unit = "1kg", UnitPrice = 3.50m },
                new Product { ProductId = 8, ItemName = "Carrots", Unit = "1kg", UnitPrice = 2.00m },
                new Product { ProductId = 9, ItemName = "Iceburg Lettuce", Unit = "1", UnitPrice = 2.50m },
                new Product { ProductId = 10, ItemName = "Helga's Wholemeal", Unit = "1", UnitPrice = 3.70m },
                new Product { ProductId = 11, ItemName = "Free range chicken", Unit = "1kg", UnitPrice = 7.50m },
                new Product { ProductId = 12, ItemName = "Manning Valley 6-pk", Unit = "6 eggs", UnitPrice = 3.60m },
                new Product { ProductId = 13, ItemName = "A2 light milk", Unit = "1 litre", UnitPrice = 2.90m },
                new Product { ProductId = 14, ItemName = "Chobani Strawberry Yoghurt", Unit = "1", UnitPrice = 1.50m },
                new Product { ProductId = 15, ItemName = "Lurpak Salted Blend", Unit = "250g", UnitPrice = 5.00m },
                new Product { ProductId = 16, ItemName = "Bega Farmers Tasty", Unit = "250g", UnitPrice = 4.00m },
                new Product { ProductId = 17, ItemName = "Babybel Mini", Unit = "100g", UnitPrice = 4.20m },
                new Product { ProductId = 18, ItemName = "Cobram EVOO", Unit = "375ml", UnitPrice = 8.00m },
                new Product { ProductId = 19, ItemName = "Heinz Tomato Soup", Unit = "535g", UnitPrice = 2.50m },
                new Product { ProductId = 20, ItemName = "John West Tuna can", Unit = "95g", UnitPrice = 1.50m },
                new Product { ProductId = 21, ItemName = "Cadbury Dairy Milk", Unit = "200g", UnitPrice = 5.00m },
                new Product { ProductId = 22, ItemName = "Coca Cola", Unit = "2 litre", UnitPrice = 2.85m },
                new Product { ProductId = 23, ItemName = "Smith's Original Share Pack Crisps", Unit = "170g", UnitPrice = 3.29m },
                new Product { ProductId = 24, ItemName = "Birds Eye Fish Fingers", Unit = "375g", UnitPrice = 4.50m },
                new Product { ProductId = 25, ItemName = "Berri Orange Juice", Unit = "2 litre", UnitPrice = 6.00m },
                new Product { ProductId = 26, ItemName = "Vegemite", Unit = "380g", UnitPrice = 6.00m },
                new Product { ProductId = 27, ItemName = "Cheddar Shapes", Unit = "175g", UnitPrice = 2.00m },
                new Product { ProductId = 28, ItemName = "Colgate Total Toothpaste Original", Unit = "110g", UnitPrice = 3.50m },
                new Product { ProductId = 29, ItemName = "Milo Chocolate Malt", Unit = "200g", UnitPrice = 4.00m },
                new Product { ProductId = 30, ItemName = "Weet Bix Sanitarium Organic", Unit = "750g", UnitPrice = 5.33m },
                new Product { ProductId = 31, ItemName = "Lindt Excellence 70% Cocoa Block", Unit = "100g", UnitPrice = 4.25m },
                new Product { ProductId = 32, ItemName = "Original Tim Tams Chocolate", Unit = "200g", UnitPrice = 3.65m },
                new Product { ProductId = 33, ItemName = "Philadelphia Original Cream Cheese", Unit = "250g", UnitPrice = 4.30m },
                new Product { ProductId = 34, ItemName = "Moccona Classic Instant Medium Roast", Unit = "100g", UnitPrice = 6.00m },
                new Product { ProductId = 35, ItemName = "Capilano Squeezable Honey", Unit = "500g", UnitPrice = 7.35m },
                new Product { ProductId = 36, ItemName = "Nutella jar", Unit = "400g", UnitPrice = 4.00m },
                new Product { ProductId = 37, ItemName = "Arnott's Scotch Finger", Unit = "250g", UnitPrice = 2.85m },
                new Product { ProductId = 38, ItemName = "South Cape Greek Feta", Unit = "200g", UnitPrice = 5.00m },
                new Product { ProductId = 39, ItemName = "Sacla Pasta Tomato Basil Sauce", Unit = "420g", UnitPrice = 4.50m },
                new Product { ProductId = 40, ItemName = "Primo English Ham", Unit = "100g", UnitPrice = 3.00m },
                new Product { ProductId = 41, ItemName = "Primo Short cut rindless Bacon", Unit = "175g", UnitPrice = 5.00m },
                new Product { ProductId = 42, ItemName = "Golden Circle Pineapple Pieces in natural juice", Unit = "440g", UnitPrice = 3.25m },
                new Product { ProductId = 43, ItemName = "San Remo Linguine Pasta No 1", Unit = "500g", UnitPrice = 1.95m }
            );

            // Seed users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Email = "user1@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password1") },
                new User { UserId = 2, Email = "user2@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password2") },
                new User { UserId = 3, Email = "user3@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password3") }
            );
        }
    }
}
