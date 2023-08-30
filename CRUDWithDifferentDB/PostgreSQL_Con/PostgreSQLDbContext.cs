using System;
using System.Collections.Generic;
using CRUDWithDifferentDB.PostgreSQL_Con.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDWithDifferentDB.PostgreSQL_Con;

public partial class PostgreSQLDbContext : DbContext
{
    public PostgreSQLDbContext()
    {
    }

    public PostgreSQLDbContext(DbContextOptions<PostgreSQLDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblSectionInformation> TblSectionInformations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=10.209.99.117; Database=PGDB; Username=ibos; Password=ibos@123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblSectionInformation>(entity =>
        {
            entity.HasKey(e => e.IntAutoId).HasName("TblSectionInformation_pkey");

            entity.ToTable("TblSectionInformation", "tst");

            entity.Property(e => e.IntAutoId).HasColumnName("intAutoId");
            entity.Property(e => e.DteActionTime).HasColumnName("dteActionTime    ");
            entity.Property(e => e.NumReadingValue).HasColumnName("numReadingValue    ");
            entity.Property(e => e.StrBusinessUnit).HasColumnName("strBusinessUnit");
            entity.Property(e => e.StrReadingOf).HasColumnName("strReadingOf    ");
            entity.Property(e => e.StrSectionName).HasColumnName("strSectionName    ");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
