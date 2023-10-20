using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AeFLOWER.Models;

public partial class FlowershopContext : DbContext
{
    public FlowershopContext()
    {
    }

    public FlowershopContext(DbContextOptions<FlowershopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=flowershop;Trusted_Connection=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA5A670FD2F0C");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.AccountDes)
                .HasMaxLength(100)
                .HasColumnName("account_des");
            entity.Property(e => e.Address).HasMaxLength(256);
            entity.Property(e => e.Country).HasMaxLength(30);
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.IdCartItem).HasName("PK__CartItem__8E5A0FCE6AE025B3");

            entity.ToTable("CartItem");

            entity.Property(e => e.IdCartItem).HasColumnName("id_CartItem");
            entity.Property(e => e.CommentPro).HasMaxLength(100);
            entity.Property(e => e.IdProduct)
                .HasMaxLength(30)
                .HasColumnName("id_product");
            entity.Property(e => e.IdShoppingCart).HasColumnName("id_shoppingCart");
            entity.Property(e => e.Price)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("price");
            entity.Property(e => e.QuantityItem).HasColumnName("quantity_item");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_id");

            entity.HasOne(d => d.IdShoppingCartNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.IdShoppingCart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shoppingcart_id");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__Category__E548B673E2438A09");

            entity.ToTable("Category");

            entity.Property(e => e.IdCategory)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("id_category");
            entity.Property(e => e.CssClass)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NameCategory)
                .HasMaxLength(30)
                .HasColumnName("name_category");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__Product__BA39E84FAC28F6B3");

            entity.ToTable("Product");

            entity.Property(e => e.IdProduct)
                .HasMaxLength(30)
                .HasColumnName("id_product");
            entity.Property(e => e.Desproduct).HasColumnName("desproduct");
            entity.Property(e => e.IdCate)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("id_cate");
            entity.Property(e => e.Imageurl)
                .IsUnicode(false)
                .HasColumnName("imageurl");
            entity.Property(e => e.MainFlower)
                .HasMaxLength(100)
                .HasColumnName("main_flower");
            entity.Property(e => e.NameProduct).HasColumnName("name_product");
            entity.Property(e => e.Newcash).HasColumnName("newcash");
            entity.Property(e => e.Oldcash).HasColumnName("oldcash");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Size)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("size");
            entity.Property(e => e.StarSum).HasColumnName("star_sum");
            entity.Property(e => e.SubFlower)
                .HasMaxLength(100)
                .HasColumnName("sub_flower");

            entity.HasOne(d => d.IdCateNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCate)
                .HasConstraintName("fk_product_category");
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("PK__Shopping__C71FE31796ABC06E");

            entity.ToTable("ShoppingCart");

            entity.Property(e => e.IdCart).HasColumnName("id_cart");
            entity.Property(e => e.CartTime)
                .HasColumnType("smalldatetime")
                .HasColumnName("cart_time");
            entity.Property(e => e.IdUser)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id_user");
            entity.Property(e => e.NameCusNonAccount)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNonAccount)
                .HasMaxLength(11)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_account_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
