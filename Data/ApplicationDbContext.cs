using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeSpokedBikes.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>()
                .HasMany<Sale>(e => e.Purchases)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.Customer_Id)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Customer>()
                .ToTable("Customers");

            builder.Entity<Discount>()
                .HasMany<SaleItemDiscount>(e => e.AppliedDiscounts)
                .WithOne(e => e.Discount)
                .HasForeignKey(e => e.Discount_Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Manager>()
                .HasMany<ManagerSalesPerson>(e => e.ManagerSalesPeople)
                .WithOne(e => e.Manager)
                .HasForeignKey(e => e.Manager_Id)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Manager>()
                .ToTable("Managers");

            builder.Entity<ManagerSalesPerson>()
                .HasKey(msp => new { msp.Manager_Id, msp.SalesPerson_Id });

            builder.Entity<Manufacturer>()
                .HasMany<Product>(e => e.Products)
                .WithOne(e => e.Manufacturer)
                .HasForeignKey(e => e.Manufacturer_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>()
                .HasMany<ProductStyle>(e => e.Styles)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.Product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>()
                .HasMany<Discount>(e => e.Discounts)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.Product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductStyle>()
                .HasMany<SaleItem>(e => e.SaleItems)
                .WithOne(e => e.ProductStyle)
                .HasForeignKey(e => e.ProductStyle_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Sale>()
                .HasMany<SaleItem>(e => e.Items)
                .WithOne(e => e.Sale)
                .HasForeignKey(e => e.Sale_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SaleItem>()
                .HasMany<SaleItemDiscount>(e => e.AppliedDiscounts)
                .WithOne(e => e.SaleItem)
                .HasForeignKey(e => e.SaleItem_Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SalesPerson>()
                .HasMany<Sale>(e => e.Sales)
                .WithOne(e => e.SalesPerson)
                .HasForeignKey(e => e.SalesPerson_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SalesPerson>()
                .HasMany<ManagerSalesPerson>(e => e.SalesPersonManagers)
                .WithOne(e => e.SalesPerson)
                .HasForeignKey(e => e.SalesPerson_Id)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<SalesPerson>()
                .ToTable("SalesPeople");
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<ManagerSalesPerson> ManagerSalesPeople { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStyle> ProductStyles { get; set; }
        public DbSet<SalesPerson> SalesPeople { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<SaleItemDiscount> SaleItemDiscounts { get; set; }
    }
}
