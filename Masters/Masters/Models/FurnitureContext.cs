using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Masters.Models;

public partial class FurnitureContext : DbContext
{
    public FurnitureContext()
    {
    }

    public FurnitureContext(DbContextOptions<FurnitureContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryStore> CategoryStores { get; set; }

    public virtual DbSet<Checkout> Checkouts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Rahma> Rahmas { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Tetamonial> Tetamonials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Furniture;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.RoleId).HasDefaultValueSql("(N'')");
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3213E83FE177F33B");

            entity.ToTable("Cart");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("User_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("prodFS_pk");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("UserFS_pk");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3213E83F99410652");

            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .IsUnicode(false)
                .HasColumnName("Category_Name");
            entity.Property(e => e.ImagePath)
                .IsUnicode(false)
                .HasColumnName("Image_Path");
        });

        modelBuilder.Entity<CategoryStore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3213E83F7DBB9107");

            entity.ToTable("Category_Store");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CatId).HasColumnName("Cat_Id");
            entity.Property(e => e.StoreId).HasColumnName("Store_Id");

            entity.HasOne(d => d.Cat).WithMany(p => p.CategoryStores)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Cat_fk");

            entity.HasOne(d => d.Store).WithMany(p => p.CategoryStores)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Store_fk");
        });

        modelBuilder.Entity<Checkout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Checkout__3213E83FBFE2C74C");

            entity.ToTable("Checkout");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fname)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Lname)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("User_id");

            entity.HasOne(d => d.User).WithMany(p => p.Checkouts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("UserFS12_pk");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3213E83FE50C3241");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOfOrder).HasColumnType("date");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("User_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("prodFS122_pk");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("UserFS222_pk");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3213E83FDA88195B");

            entity.ToTable("Payment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Cvv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cvv");
            entity.Property(e => e.NameOnCard)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3213E83FF8107B6D");

            entity.ToTable("Product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryStoreId).HasColumnName("Category_Store_id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ImagePath)
                .IsUnicode(false)
                .HasColumnName("Image_Path");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.CategoryStore).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryStoreId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("cateFS_pk");
        });

        modelBuilder.Entity<Rahma>(entity =>
        {
            entity.ToTable("rahma");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Store__3213E83FFB2BE391");

            entity.ToTable("Store");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImagePath)
                .IsUnicode(false)
                .HasColumnName("Image_Path");
            entity.Property(e => e.StatusPublish)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("statusPublish");
            entity.Property(e => e.StoreName)
                .IsUnicode(false)
                .HasColumnName("Store_Name");
        });

        modelBuilder.Entity<Tetamonial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tetamoni__3213E83F8C82EFEB");

            entity.ToTable("Tetamonial");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentUser).IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("User_id");

            entity.HasOne(d => d.User).WithMany(p => p.Tetamonials)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("UserFSTes_pk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
