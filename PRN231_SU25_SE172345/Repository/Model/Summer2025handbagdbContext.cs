using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repository.Model;

public partial class Summer2025handbagdbContext : DbContext
{
    public Summer2025handbagdbContext()
    {
    }

    public Summer2025handbagdbContext(DbContextOptions<Summer2025handbagdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Handbag> Handbags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=123456;database= summer2025handbagdb;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brands__3214EC07463892D7");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Handbag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Handbags__3214EC07E837EA83");

            entity.Property(e => e.Material).HasMaxLength(50);
            entity.Property(e => e.ModelName).HasMaxLength(100);

            entity.HasOne(d => d.Brand).WithMany(p => p.Handbags)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Handbags__BrandI__267ABA7A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07425DCE0F");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
