using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RoyalHall.Models;

namespace RoyalHall.Data;

public partial class BookingHallContext : DbContext
{
    public BookingHallContext()
    {
    }

    public BookingHallContext(DbContextOptions<BookingHallContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Hall> Halls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-2AR5OF7M;Database=BookingHall;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951AED1EF98D3A");

            entity.ToTable("Booking");


            entity.Property(e => e.BookingType)
                .HasMaxLength(100)
                .HasColumnName("BookingType");

            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D86F06B52A");

            entity.ToTable("Customer");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<Hall>(entity =>
        {
            //entity.HasKey(e => e.HallId).HasName("PK__Hall__7E60E2140F8F9C90");


            entity.ToTable("Hall");

            //entity.Property(e => e.HallId).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
