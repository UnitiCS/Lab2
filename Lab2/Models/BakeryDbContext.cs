using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Models;

public partial class BakeryDbContext : DbContext
{
    public BakeryDbContext()
    {
    }

    public BakeryDbContext(DbContextOptions<BakeryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BakeryProduct> BakeryProducts { get; set; }

    public virtual DbSet<BreadRecipe> BreadRecipes { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<ProductsWithIngredient> ProductsWithIngredients { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=BakeryDB1;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BakeryProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__BakeryPr__B40CC6ED3F41BC01");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.BreadRecipeId).HasColumnName("BreadRecipeID");
            entity.Property(e => e.Description).HasMaxLength(60);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.SupplyId).HasColumnName("SupplyID");
            entity.Property(e => e.Type).HasMaxLength(30);
        });

        modelBuilder.Entity<BreadRecipe>(entity =>
        {
            entity.HasKey(e => e.BreadRecipeId).HasName("PK__BreadRec__40E7FC5A0FDDF912");

            entity.Property(e => e.BreadRecipeId).HasColumnName("BreadRecipeID");
            entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
            entity.Property(e => e.IngredientName).HasMaxLength(30);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductName).HasMaxLength(30);

            entity.HasOne(d => d.Ingredient).WithMany(p => p.BreadRecipes)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BreadRecipes_Ingredients");

            entity.HasOne(d => d.Product).WithMany(p => p.BreadRecipes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BreadRecipes_BakeryProducts");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK__Ingredie__BEAEB27AB24A1C39");

            entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Type).HasMaxLength(30);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF9A4013BF");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerName).HasMaxLength(30);
            entity.Property(e => e.DeliveryDate).HasColumnType("date");
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductName).HasMaxLength(30);
            entity.Property(e => e.ProductType).HasMaxLength(30);

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Orders_BakeryProducts");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OrderDetails");

            entity.Property(e => e.CustomerName).HasMaxLength(30);
            entity.Property(e => e.DeliveryDate).HasColumnType("date");
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(30);
            entity.Property(e => e.ProductType).HasMaxLength(30);
            entity.Property(e => e.Supplier).HasMaxLength(30);
            entity.Property(e => e.SupplyDate).HasColumnType("date");
            entity.Property(e => e.SupplyPrice).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<ProductsWithIngredient>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductsWithIngredients");

            entity.Property(e => e.IngredientName).HasMaxLength(30);
            entity.Property(e => e.IngredientType).HasMaxLength(30);
            entity.Property(e => e.ProductDescription).HasMaxLength(60);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductName).HasMaxLength(30);
            entity.Property(e => e.ProductType).HasMaxLength(30);
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.SupplyId).HasName("PK__Supplies__7CDD6C8E22D69092");

            entity.Property(e => e.SupplyId).HasColumnName("SupplyID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductName).HasMaxLength(30);
            entity.Property(e => e.Supplier).HasMaxLength(30);
            entity.Property(e => e.SupplyDate).HasColumnType("date");

            entity.HasOne(d => d.Product).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Supplies_BakeryProducts");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
