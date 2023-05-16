using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVC_FIRST.Models;

public partial class todoContext : DbContext
{
    public todoContext(DbContextOptions<todoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customers> Customers { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customers>(entity =>
        {
            entity.HasKey(e => e.CustomerID).HasName("PK__Customer__A4AE64B8BB3E92BA");

            entity.Property(e => e.CustomerID).ValueGeneratedNever();
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.OrderID).HasName("PK__Orders__C3905BAF60C89043");

            entity.Property(e => e.OrderID).ValueGeneratedNever();
            entity.Property(e => e.OrderDate).HasColumnType("date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
