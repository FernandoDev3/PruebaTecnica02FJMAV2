using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PruebaTecnica02FJMAV2.Models
{
    public partial class PruebaTecnica02FJMAV2Context : DbContext
    {
        public PruebaTecnica02FJMAV2Context()
        {
        }

        public PruebaTecnica02FJMAV2Context(DbContextOptions<PruebaTecnica02FJMAV2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Celulare> Celulares { get; set; } = null!;
        public virtual DbSet<Marca> Marcas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-9JIAMII;Initial Catalog=PruebaTecnica02FJMAV2;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Celulare>(entity =>
            {
                entity.HasKey(e => e.CelularId)
                    .HasName("PK__Celulare__8B77D10ACC30E82A");

                entity.Property(e => e.Imagen).HasColumnType("image");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.Celulares)
                    .HasForeignKey(d => d.MarcaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Celulares__Marca__398D8EEE");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
