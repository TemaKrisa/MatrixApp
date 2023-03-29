using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Matrix.Model;

public partial class CosmeticshopContext : DbContext
{
    public CosmeticshopContext()
    {
    }

    public CosmeticshopContext(DbContextOptions<CosmeticshopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<DeliveryPoint> DeliveryPoints { get; set; }

    public virtual DbSet<DeliveryType> DeliveryTypes { get; set; }

    public virtual DbSet<EmailDatum> EmailData { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Usertype> Usertypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=www.db4free.net;port=3306;database=cosmeticshop;user=user1468;password=wesdxcrfv1q", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql")).EnableSensitiveDataLogging();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Usertype, "AccountToUsertype");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(75)
                .UseCollation("utf8mb4_bin");
            entity.Property(e => e.Flat)
                .HasMaxLength(5)
                .UseCollation("utf8mb4_bin");
            entity.Property(e => e.House)
                .HasMaxLength(75)
                .UseCollation("utf8mb4_bin");
            entity.Property(e => e.Midname)
                .HasMaxLength(50)
                .UseCollation("utf8mb4_bin");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .UseCollation("utf8mb4_bin");
            entity.Property(e => e.Password)
                .HasMaxLength(75)
                .UseCollation("utf8mb4_bin");
            entity.Property(e => e.Phone).HasPrecision(20);
            entity.Property(e => e.Street)
                .HasMaxLength(75)
                .UseCollation("utf8mb4_bin");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .UseCollation("utf8mb4_bin");

            entity.HasOne(d => d.UsertypeNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Usertype)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AccountToUsertype");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PRIMARY");

            entity.ToTable("Brand");

            entity.HasIndex(e => e.Manufacturer, "BrendToManufacturer");

            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.BrandName)
                .HasMaxLength(75)
                .UseCollation("utf8mb4_bin");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Brands)
                .HasForeignKey(d => d.Manufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BrendToManufacturer");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.UserId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Cart");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CartToProduct");
        });

        modelBuilder.Entity<DeliveryPoint>(entity =>
        {
            entity.HasKey(e => e.PointId).HasName("PRIMARY");

            entity.ToTable("DeliveryPoint");

            entity.Property(e => e.PointId).HasColumnName("PointID");
            entity.Property(e => e.House)
                .HasMaxLength(5)
                .UseCollation("utf8mb4_bin");
            entity.Property(e => e.Street)
                .HasMaxLength(30)
                .UseCollation("utf8mb4_bin");
        });

        modelBuilder.Entity<DeliveryType>(entity =>
        {
            entity.HasKey(e => e.TypeiD).HasName("PRIMARY");

            entity.ToTable("DeliveryType");

            entity.Property(e => e.TypeName)
                .HasMaxLength(75)
                .UseCollation("utf8mb4_bin");
        });

        modelBuilder.Entity<EmailDatum>(entity =>
        {
            entity.HasKey(e => e.EmailAdress).HasName("PRIMARY");

            entity.Property(e => e.EmailAdress)
                .HasMaxLength(300)
                .UseCollation("utf8mb4_bin");
            entity.Property(e => e.AuthToken)
                .HasMaxLength(300)
                .UseCollation("utf8mb4_bin");
            entity.Property(e => e.Host)
                .HasMaxLength(300)
                .UseCollation("utf8mb4_bin");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("PRIMARY");

            entity.ToTable("Manufacturer");

            entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");
            entity.Property(e => e.ManufacturerName)
                .HasMaxLength(75)
                .UseCollation("utf8mb4_bin");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("Order");

            entity.HasIndex(e => e.DeliveryType, "DeliveryToOrder");

            entity.HasIndex(e => e.Client, "OrderToClient");

            entity.HasIndex(e => e.Status, "OrderToStatus");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.TotalSum).HasPrecision(10);

            entity.HasOne(d => d.ClientNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Client)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderToClient");

            entity.HasOne(d => d.DeliveryTypeNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeliveryType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DeliveryToOrder");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderToStatus");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(e => new { e.Order, e.Product })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.HasIndex(e => e.Product, "OrderToProduct");

            entity.HasOne(d => d.OrderNavigation).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.Order)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderProductToOrder");

            entity.HasOne(d => d.ProductNavigation).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.Product)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderToProduct");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("PRIMARY");

            entity.ToTable("OrderStatus");

            entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");
            entity.Property(e => e.OrderStatusName)
                .HasMaxLength(75)
                .UseCollation("utf8mb4_bin");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("Product");

            entity.HasIndex(e => e.Category, "ProductCategory");

            entity.HasIndex(e => e.Brand, "ProductToBrend");

            entity.HasIndex(e => e.Manufacturer, "ProductToManufacturer");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .UseCollation("utf8mb4_bin");
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.ProductName)
                .HasMaxLength(150)
                .UseCollation("utf8mb4_bin");
            entity.Property(e => e.TotalPrice).HasPrecision(10, 2);

            entity.HasOne(d => d.BrandNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Brand)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductToBrend");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Category)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductCategory");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Manufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductToManufacturer");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("ProductCategory");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(75)
                .UseCollation("utf8mb4_bin");
        });

        modelBuilder.Entity<Usertype>(entity =>
        {
            entity.HasKey(e => e.UsertypeId).HasName("PRIMARY");

            entity.ToTable("Usertype");

            entity.Property(e => e.UsertypeId).HasColumnName("UsertypeID");
            entity.Property(e => e.UsertypeName)
                .HasMaxLength(75)
                .UseCollation("utf8mb4_bin");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
