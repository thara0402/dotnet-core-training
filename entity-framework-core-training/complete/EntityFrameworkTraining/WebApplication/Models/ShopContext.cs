using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketItem> BasketItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        // Labo #1 ---------------------------- ↓
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Desc)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Price).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Ts)
                    .HasColumnName("TS")
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.Property(e => e.BasketId).HasColumnName("BasketID");

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .HasColumnName("TS")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<BasketItem>(entity =>
            {
                entity.Property(e => e.BasketItemId).HasColumnName("BasketItemID");

                entity.Property(e => e.BasketId).HasColumnName("BasketID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Ts)
                    .HasColumnName("TS")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(28, 6)");

                entity.HasOne(d => d.Basket)
                    .WithMany(p => p.BasketItem)
                    .HasForeignKey(d => d.BasketId)
                    .HasConstraintName("FK_BasketItem_Basket_Shop_Basket");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.BasketItem)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BasketItem_Product_Shop_Product");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .HasColumnName("TS")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Ts)
                    .HasColumnName("TS")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(28, 6)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_Order_Shop_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_Product_Shop_Product");
            });
        }
        // Labo #1 ---------------------------- ↑
    }
}
