using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OfflineSyncApi.OfflineSyncContext
{
    public partial class VentasContext : DbContext
    {
        public VentasContext()
        {
        }

        public VentasContext(DbContextOptions<VentasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulo> Articulos { get; set; } = null!;
        public virtual DbSet<CatConflicto> CatConflictos { get; set; } = null!;
        public virtual DbSet<CatTabla> CatTablas { get; set; } = null!;
        public virtual DbSet<ResolverConflicto> ResolverConflictos { get; set; } = null!;
        public virtual DbSet<Syncoffline> Syncofflines { get; set; } = null!;
        public virtual DbSet<Tienda> Tiendas { get; set; } = null!;
        public virtual DbSet<VentasTienda> VentasTiendas { get; set; } = null!;
        public virtual DbSet<VentasTiendasSyncOfflineApp> VentasTiendasSyncOfflineApp { get; set; } = null!;
        public virtual DbSet<VentasTiendasSyncChange> VentasTiendasSyncChanges { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Ean });

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ean).HasColumnName("EAN");

                entity.Property(e => e.ArticuloHashId)
                    .HasMaxLength(8000)
                    .HasColumnName("ArticuloHashID")
                    .HasComputedColumnSql("(hashbytes('SHA2_256',(CONVERT([varchar](20),[ID])+CONVERT([varchar](20),[EAN]))+[DESCRIPCION]))", false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");
            });

            modelBuilder.Entity<CatConflicto>(entity =>
            {
                entity.HasKey(e => new { e.IdCatConflictos, e.Nombreconflicto });

                entity.ToTable("CAT_CONFLICTOS");

                entity.Property(e => e.IdCatConflictos).HasColumnName("ID_CAT_CONFLICTOS");

                entity.Property(e => e.Nombreconflicto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRECONFLICTO");
            });

            modelBuilder.Entity<CatTabla>(entity =>
            {
                entity.HasKey(e => new { e.IdCatTabla, e.NombreTabla });

                entity.ToTable("CAT_TABLAS");

                entity.Property(e => e.IdCatTabla).HasColumnName("ID_CAT_TABLA");

                entity.Property(e => e.NombreTabla)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_TABLA");

                entity.Property(e => e.FechaSync)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_SYNC");
            });

            modelBuilder.Entity<ResolverConflicto>(entity =>
            {
                entity.HasKey(e => e.IdResolverConflictos);

                entity.ToTable("RESOLVER_CONFLICTOS");

                entity.Property(e => e.IdResolverConflictos).HasColumnName("ID_RESOLVER_CONFLICTOS");

                entity.Property(e => e.IdCatConflictos).HasColumnName("ID_CAT_CONFLICTOS");

                entity.Property(e => e.IdCatTabla).HasColumnName("ID_CAT_TABLA");

                entity.Property(e => e.Resuelto)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RESUELTO");
            });

            modelBuilder.Entity<Syncoffline>(entity =>
            {
                entity.HasKey(e => e.IdSyncoffline);

                entity.ToTable("SYNCOFFLINE");

                entity.Property(e => e.IdSyncoffline).HasColumnName("ID_SYNCOFFLINE");

                entity.Property(e => e.Fechasync)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHASYNC");

                entity.Property(e => e.IdTabla).HasColumnName("ID_TABLA");
            });

            modelBuilder.Entity<Tienda>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.Tienda1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TIENDA");

                entity.Property(e => e.TiendaHashId)
                    .HasMaxLength(8000)
                    .HasColumnName("TiendaHashID")
                    .HasComputedColumnSql("(hashbytes('SHA2_256',(CONVERT([varchar](20),[ID])+[TIENDA])+[DIRECCION]))", false);
            });

            modelBuilder.Entity<VentasTienda>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Articulo).HasColumnName("ARTICULO");

                entity.Property(e => e.Fecha)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("FECHA");

                entity.Property(e => e.Tienda).HasColumnName("TIENDA");

                entity.Property(e => e.Venta).HasColumnName("VENTA");

                entity.Property(e => e.VentasTiendasHashId)
                    .HasMaxLength(8000)
                    .HasColumnName("VentasTiendasHashID")
                    .HasComputedColumnSql("(hashbytes('SHA2_256',(([FECHA]+CONVERT([varchar](20),[TIENDA]))+CONVERT([varchar](20),[ARTICULO]))+CONVERT([varchar](20),[VENTA])))", false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
