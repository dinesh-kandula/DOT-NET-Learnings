using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirstApproach.Models;

public partial class ZeptoContext : DbContext
{
    public ZeptoContext()
    {
    }

    public ZeptoContext(DbContextOptions<ZeptoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<PizzaSpecial> PizzaSpecials { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ZeptoUser> ZeptoUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.HasIndex(e => e.ZeptoUserId, "IX_Cart_ZeptoUserId").IsUnique();

            entity.HasOne(d => d.ZeptoUser).WithOne(p => p.Cart).HasForeignKey<Cart>(d => d.ZeptoUserId);

            entity.HasMany(d => d.Products).WithMany(p => p.Carts)
                .UsingEntity<Dictionary<string, object>>(
                    "CartProduct",
                    r => r.HasOne<Product>().WithMany().HasForeignKey("ProductsId"),
                    l => l.HasOne<Cart>().WithMany().HasForeignKey("CartsId"),
                    j =>
                    {
                        j.HasKey("CartsId", "ProductsId");
                        j.ToTable("CartProduct");
                        j.HasIndex(new[] { "ProductsId" }, "IX_CartProduct_ProductsId");
                    });
        });

        modelBuilder.Entity<PizzaSpecial>(entity =>
        {
            entity.Property(e => e.BasePrice).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.BasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Offer).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Quantity).HasDefaultValue("");
        });

        modelBuilder.Entity<ZeptoUser>(entity =>
        {
            entity.ToTable("ZeptoUser");

            entity.HasIndex(e => e.Email, "IX_ZeptoUser_Email").IsUnique();

            entity.HasIndex(e => e.UserName, "IX_ZeptoUser_UserName").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
