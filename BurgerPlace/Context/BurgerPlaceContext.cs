using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BurgerPlace.Models.Database;

namespace BurgerPlace.Context
{
    public partial class BurgerPlaceContext : DbContext
    {
        public BurgerPlaceContext()
        {
        }

        public BurgerPlaceContext(DbContextOptions<BurgerPlaceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public virtual DbSet<Photo> Photos { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductIngredient> ProductIngredients { get; set; } = null!;
        public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;
        public virtual DbSet<Set> Sets { get; set; } = null!;
        public virtual DbSet<SetCategory> SetCategories { get; set; } = null!;
        public virtual DbSet<SetProduct> SetProducts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=burgerplace;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.24-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_polish_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.HasComment("Table to store address informations");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.FlatNumber)
                    .HasMaxLength(20)
                    .HasColumnName("flat_number");

                entity.Property(e => e.Street)
                    .HasMaxLength(50)
                    .HasColumnName("street");

                entity.Property(e => e.StreetNumber)
                    .HasMaxLength(20)
                    .HasColumnName("street_number");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.HasComment("Table for storing categories");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(24)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("ingredient");

                entity.HasComment("Table for storing ingredients");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(5,2) unsigned")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasComment("Table to store orders");

                entity.HasIndex(e => e.AddressId, "FK_order_address");

                entity.HasIndex(e => e.RestaurantId, "FK_order_restaurant");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AddressId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("address_id");

                entity.Property(e => e.IsPickup).HasColumnName("is_pickup");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .HasColumnName("phone");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(8,2) unsigned")
                    .HasColumnName("price");

                entity.Property(e => e.RestaurantId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("restaurant_id");

                entity.Property(e => e.Surname)
                    .HasMaxLength(30)
                    .HasColumnName("surname");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_order_address");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_restaurant");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.ToTable("order_product");

                entity.HasIndex(e => e.OrderId, "FK_order_product_order");

                entity.HasIndex(e => e.ProductId, "FK_order_product_product");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("order_id");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("product_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_product_order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_order_product_product");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("photo");

                entity.HasComment("Table for storing photos");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Path)
                    .HasMaxLength(120)
                    .HasColumnName("path");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasComment("Table that will store products and all informations connected with it");

                entity.HasIndex(e => e.PhotoId, "FK_product_photo");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Available).HasColumnName("available");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .HasColumnName("name");

                entity.Property(e => e.PhotoId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("photo_id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(8,2) unsigned")
                    .HasColumnName("price");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("FK_product_photo");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("product_category");

                entity.HasComment("Table for storing categories of products");

                entity.HasIndex(e => e.CategoryId, "FK_product_category_category");

                entity.HasIndex(e => e.ProductId, "FK_product_category_product");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("category_id");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("product_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_product_category_category");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_product_category_product");
            });

            modelBuilder.Entity<ProductIngredient>(entity =>
            {
                entity.ToTable("product_ingredient");

                entity.HasComment("Table for storing ingredients in products");

                entity.HasIndex(e => e.IngredientId, "FK_product_ingredient_ingredient");

                entity.HasIndex(e => e.ProductId, "FK_product_ingredient_product");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.IngredientId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("ingredient_id");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("product_id");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.ProductIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .HasConstraintName("FK_product_ingredient_ingredient");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductIngredients)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_product_ingredient_product");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("restaurant");

                entity.HasComment("Table to store restaurants data");

                entity.HasIndex(e => e.Address, "FK_restaurant_address");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.HasOne(d => d.AddressNavigation)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.Address)
                    .HasConstraintName("FK_restaurant_address");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.ToTable("set");

                entity.HasComment("Set of products like for ex. Happy Meal");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Available).HasColumnName("available");

                entity.Property(e => e.Comment)
                    .HasMaxLength(50)
                    .HasColumnName("comment");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(8,2) unsigned")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<SetCategory>(entity =>
            {
                entity.ToTable("set_category");

                entity.HasComment("Table to store categories of set");

                entity.HasIndex(e => e.CategoryId, "FK_set_category_category");

                entity.HasIndex(e => e.SetId, "FK_set_category_set");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned zerofill")
                    .HasColumnName("id");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("category_id");

                entity.Property(e => e.SetId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("set_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SetCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_set_category_category");

                entity.HasOne(d => d.Set)
                    .WithMany(p => p.SetCategories)
                    .HasForeignKey(d => d.SetId)
                    .HasConstraintName("FK_set_category_set");
            });

            modelBuilder.Entity<SetProduct>(entity =>
            {
                entity.ToTable("set_product");

                entity.HasComment("Table to store products in set");

                entity.HasIndex(e => e.ProductId, "FK_set_product_product");

                entity.HasIndex(e => e.SetId, "FK_set_product_set");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("product_id");

                entity.Property(e => e.SetId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("set_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SetProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_set_product_product");

                entity.HasOne(d => d.Set)
                    .WithMany(p => p.SetProducts)
                    .HasForeignKey(d => d.SetId)
                    .HasConstraintName("FK_set_product_set");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasComment("Table for storing users");

                entity.HasIndex(e => e.RestaurantId, "FK_user_restaurant");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.IsRoot).HasColumnName("is_root");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime")
                    .HasColumnName("last_login")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .HasColumnName("password");

                entity.Property(e => e.RestaurantId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("restaurant_id");

                entity.Property(e => e.Surname)
                    .HasMaxLength(32)
                    .HasColumnName("surname");

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .HasColumnName("username");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_user_restaurant");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
