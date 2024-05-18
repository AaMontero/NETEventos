using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EventosServicio.Models
{
    public partial class pruebaTecnicaDBContext : DbContext
    {
        public pruebaTecnicaDBContext()
        {
        }

        public pruebaTecnicaDBContext(DbContextOptions<pruebaTecnicaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Evento> Eventos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DescripcionEvento).HasColumnName("descripcion_evento");

                entity.Property(e => e.Eliminado)
                    .HasColumnName("eliminado")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FechaEvento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_evento");

                entity.Property(e => e.LugarEvento)
                    .HasMaxLength(255)
                    .HasColumnName("lugar_evento");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
