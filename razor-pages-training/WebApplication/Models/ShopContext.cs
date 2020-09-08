using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication.Models
{
    public partial class ShopContext : DbContext
    {
        public ShopContext()
        {
        }

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Basket> Basket { get; set; }
        public virtual DbSet<BasketItem> BasketItem { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Shop;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
