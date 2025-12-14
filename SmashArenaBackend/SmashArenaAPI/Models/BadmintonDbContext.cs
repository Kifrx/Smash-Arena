using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmashArenaAPI.Models;

namespace SmashArenaAPI.Models;

public partial class BadmintonDbContext : DbContext
{
    public BadmintonDbContext()
    {
    }

    public BadmintonDbContext(DbContextOptions<BadmintonDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }
    public virtual DbSet<Court> Courts { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PRIMARY"); 

            entity.ToTable("booking");

            entity.HasIndex(e => e.CourtId, "Court_ID");
            entity.HasIndex(e => e.UserId, "User_ID");

            entity.Property(e => e.BookingId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Booking_ID"); 

            entity.Property(e => e.CourtId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Court_ID");

            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("User_ID");

            entity.Property(e => e.JamMulai)
                .HasColumnType("time")
                .HasColumnName("Jam_Mulai");

            entity.Property(e => e.JamSelesai)
                .HasColumnType("time")
                .HasColumnName("Jam_Selesai");

            entity.Property(e => e.StatusBooking)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("Status_Booking");

            entity.Property(e => e.TanggalMain).HasColumnName("Tanggal_Main");

            entity.Property(e => e.TotalHarga)
                .HasPrecision(10, 2)
                .HasColumnName("Total_Harga");
            
            entity.Property(e => e.WaktuDibuat)
                .HasColumnType("datetime")
                .HasColumnName("Waktu_Dibuat");

            entity.HasOne(d => d.Court).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("booking_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("booking_ibfk_1");
        });


        modelBuilder.Entity<Court>(entity =>
        {
            entity.HasKey(e => e.CourtId).HasName("PRIMARY");

            entity.ToTable("court");

            entity.Property(e => e.CourtId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Court_ID"); 

            entity.Property(e => e.Fasilitas).HasMaxLength(100);

            entity.Property(e => e.GambarUrl)
                .HasMaxLength(255)
                .HasColumnName("Gambar_URL");

            entity.Property(e => e.HargaPerJam)
                .HasPrecision(10, 2)
                .HasColumnName("Harga_Per_Jam");

            entity.Property(e => e.JenisLantai)
                .HasMaxLength(50)
                .HasColumnName("Jenis_Lantai");

            entity.Property(e => e.NamaCourt)
                .HasMaxLength(50)
                .HasColumnName("Nama_Court");

            entity.Property(e => e.StatusAktif).HasColumnName("Status_Aktif");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("User_ID");
            
            entity.Property(e => e.Email).HasMaxLength(255);
            
            entity.Property(e => e.NamaLengkap)
                .HasMaxLength(255)
                .HasColumnName("Nama_Lengkap"); 

            entity.Property(e => e.NomorHp)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("Nomor_Hp");

            entity.Property(e => e.Password).HasMaxLength(255);
            
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}