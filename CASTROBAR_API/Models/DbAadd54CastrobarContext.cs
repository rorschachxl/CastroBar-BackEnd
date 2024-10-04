using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CASTROBAR_API.Models;

public partial class DbAadd54CastrobarContext : DbContext
{
    public DbAadd54CastrobarContext()
    {
    }

    public DbAadd54CastrobarContext(DbContextOptions<DbAadd54CastrobarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditorium> Auditoria { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Orden> Ordens { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoOrden> ProductoOrdens { get; set; }

    public virtual DbSet<ProductoProveedor> ProductoProveedors { get; set; }

    public virtual DbSet<ProductoRecetum> ProductoReceta { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Recetum> Receta { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Subcategorium> Subcategoria { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.IdAuditoria);

            entity.ToTable("AUDITORIA");

            entity.Property(e => e.IdAuditoria)
                .ValueGeneratedNever()
                .HasColumnName("idAuditoria");
            entity.Property(e => e.Accion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("accion");
            entity.Property(e => e.Antes)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("antes");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Despues)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("despues");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fechaHora");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.ToTable("CATEGORIA");

            entity.Property(e => e.IdCategoria)
                .ValueGeneratedNever()
                .HasColumnName("idCategoria");
            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categoria");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.IdDocumento);

            entity.ToTable("DOCUMENTO");

            entity.Property(e => e.IdDocumento)
                .ValueGeneratedNever()
                .HasColumnName("idDocumento");
            entity.Property(e => e.Documento1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("documento");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado);

            entity.ToTable("ESTADO");

            entity.Property(e => e.IdEstado)
                .ValueGeneratedNever()
                .HasColumnName("idEstado");
            entity.Property(e => e.Estado1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.NumeroMesa);

            entity.ToTable("MESA");

            entity.Property(e => e.NumeroMesa)
                .ValueGeneratedNever()
                .HasColumnName("numeroMesa");
            entity.Property(e => e.Capacidad).HasColumnName("capacidad");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago);

            entity.ToTable("METODO_PAGO");

            entity.Property(e => e.IdMetodoPago)
                .ValueGeneratedNever()
                .HasColumnName("idMetodoPago");
            entity.Property(e => e.MetodoPago1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metodoPago");
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.HasKey(e => e.IdOrden);

            entity.ToTable("ORDEN");

            entity.Property(e => e.IdOrden)
                .ValueGeneratedNever()
                .HasColumnName("idOrden");
            entity.Property(e => e.EstadoIdEstado).HasColumnName("ESTADO_idEstado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.MesaNumeroMesa).HasColumnName("MESA_numeroMesa");
            entity.Property(e => e.MetodoPagoIdMetodoPago).HasColumnName("METODO_PAGO_idMetodoPago");
            entity.Property(e => e.UsuarioIdUsuario).HasColumnName("USUARIO_idUsuario");

            entity.HasOne(d => d.EstadoIdEstadoNavigation).WithMany(p => p.Ordens)
                .HasForeignKey(d => d.EstadoIdEstado)
                .HasConstraintName("FK_ORDEN_ESTADO");

            entity.HasOne(d => d.MesaNumeroMesaNavigation).WithMany(p => p.Ordens)
                .HasForeignKey(d => d.MesaNumeroMesa)
                .HasConstraintName("FK_ORDEN_MESA");

            entity.HasOne(d => d.MetodoPagoIdMetodoPagoNavigation).WithMany(p => p.Ordens)
                .HasForeignKey(d => d.MetodoPagoIdMetodoPago)
                .HasConstraintName("FK_ORDEN_METODO_PAGO");

            entity.HasOne(d => d.UsuarioIdUsuarioNavigation).WithMany(p => p.Ordens)
                .HasForeignKey(d => d.UsuarioIdUsuario)
                .HasConstraintName("FK_ORDEN_USUARIO1");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.IdProducto)
                .ValueGeneratedNever()
                .HasColumnName("idProducto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.CategoriaIdCategoria).HasColumnName("CATEGORIA_idCategoria");
            entity.Property(e => e.DescripcionProducto)
                .HasColumnType("text")
                .HasColumnName("descripcionProducto");
            entity.Property(e => e.EstadoIdEstado).HasColumnName("ESTADO_idEstado");
            entity.Property(e => e.Imagen)
                .HasColumnType("image")
                .HasColumnName("imagen");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreProducto");
            entity.Property(e => e.PrecioCompra).HasColumnName("precioCompra");
            entity.Property(e => e.PrecioVenta).HasColumnName("precioVenta");
            entity.Property(e => e.RecetaIdReceta).HasColumnName("RECETA_idReceta");
            entity.Property(e => e.SubcategoriaIdSubcategoria).HasColumnName("SUBCATEGORIA_idSubcategoria");

            entity.HasOne(d => d.CategoriaIdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaIdCategoria)
                .HasConstraintName("FK_PRODUCTO_CATEGORIA");

            entity.HasOne(d => d.EstadoIdEstadoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.EstadoIdEstado)
                .HasConstraintName("FK_PRODUCTO_ESTADO");

            entity.HasOne(d => d.RecetaIdRecetaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.RecetaIdReceta)
                .HasConstraintName("FK_PRODUCTO_RECETA");

            entity.HasOne(d => d.SubcategoriaIdSubcategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.SubcategoriaIdSubcategoria)
                .HasConstraintName("FK_PRODUCTO_SUBCATEGORIA");
        });

        modelBuilder.Entity<ProductoOrden>(entity =>
        {
            entity.HasKey(e => e.IdProductoOrden);

            entity.ToTable("PRODUCTO_ORDEN");

            entity.Property(e => e.IdProductoOrden)
                .ValueGeneratedNever()
                .HasColumnName("idProductoOrden");
            entity.Property(e => e.OrdenIdOrden).HasColumnName("ORDEN_idOrden");
            entity.Property(e => e.ProductoIdProducto).HasColumnName("PRODUCTO_idProducto");

            entity.HasOne(d => d.OrdenIdOrdenNavigation).WithMany(p => p.ProductoOrdens)
                .HasForeignKey(d => d.OrdenIdOrden)
                .HasConstraintName("FK_PRODUCTO_ORDEN_ORDEN");

            entity.HasOne(d => d.ProductoIdProductoNavigation).WithMany(p => p.ProductoOrdens)
                .HasForeignKey(d => d.ProductoIdProducto)
                .HasConstraintName("FK_PRODUCTO_ORDEN_PRODUCTO");
        });

        modelBuilder.Entity<ProductoProveedor>(entity =>
        {
            entity.HasKey(e => e.IdProductoProveedor);

            entity.ToTable("PRODUCTO_PROVEEDOR");

            entity.Property(e => e.IdProductoProveedor)
                .ValueGeneratedNever()
                .HasColumnName("idProductoProveedor");
            entity.Property(e => e.ProductoIdProducto).HasColumnName("PRODUCTO_idProducto");
            entity.Property(e => e.ProveedorIdProveedor).HasColumnName("PROVEEDOR_idProveedor");

            entity.HasOne(d => d.ProductoIdProductoNavigation).WithMany(p => p.ProductoProveedors)
                .HasForeignKey(d => d.ProductoIdProducto)
                .HasConstraintName("FK_PRODUCTO_PROVEEDOR_PRODUCTO");

            entity.HasOne(d => d.ProveedorIdProveedorNavigation).WithMany(p => p.ProductoProveedors)
                .HasForeignKey(d => d.ProveedorIdProveedor)
                .HasConstraintName("FK_PRODUCTO_PROVEEDOR_PROVEEDOR");
        });

        modelBuilder.Entity<ProductoRecetum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PRODUCTO_RECETA");

            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProductoReceta).HasColumnName("idProductoReceta");
            entity.Property(e => e.ProductoIdProducto).HasColumnName("PRODUCTO_idProducto");
            entity.Property(e => e.RecetaIdReceta).HasColumnName("RECETA_idReceta");

            entity.HasOne(d => d.ProductoIdProductoNavigation).WithMany()
                .HasForeignKey(d => d.ProductoIdProducto)
                .HasConstraintName("FK_PRODUCTO_RECETA_PRODUCTO");

            entity.HasOne(d => d.RecetaIdRecetaNavigation).WithMany()
                .HasForeignKey(d => d.RecetaIdReceta)
                .HasConstraintName("FK_PRODUCTO_RECETA_RECETA");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor);

            entity.ToTable("PROVEEDOR");

            entity.Property(e => e.IdProveedor)
                .ValueGeneratedNever()
                .HasColumnName("idProveedor");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.DocumentoIdDocumento).HasColumnName("DOCUMENTO_idDocumento");
            entity.Property(e => e.EstadoIdEstado).HasColumnName("ESTADO_idEstado");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numeroDocumento");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.DocumentoIdDocumentoNavigation).WithMany(p => p.Proveedors)
                .HasForeignKey(d => d.DocumentoIdDocumento)
                .HasConstraintName("FK_PROVEEDOR_DOCUMENTO");

            entity.HasOne(d => d.EstadoIdEstadoNavigation).WithMany(p => p.Proveedors)
                .HasForeignKey(d => d.EstadoIdEstado)
                .HasConstraintName("FK_PROVEEDOR_ESTADO");
        });

        modelBuilder.Entity<Recetum>(entity =>
        {
            entity.HasKey(e => e.IdReceta);

            entity.ToTable("RECETA");

            entity.Property(e => e.IdReceta)
                .ValueGeneratedNever()
                .HasColumnName("id_Receta");
            entity.Property(e => e.Preparacion)
                .HasColumnType("text")
                .HasColumnName("preparacion");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("ROL");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("idRol");
            entity.Property(e => e.Rol1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rol");
        });

        modelBuilder.Entity<Subcategorium>(entity =>
        {
            entity.HasKey(e => e.IdSubcategoria);

            entity.ToTable("SUBCATEGORIA");

            entity.Property(e => e.IdSubcategoria)
                .ValueGeneratedNever()
                .HasColumnName("idSubcategoria");
            entity.Property(e => e.Subcategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("subcategoria");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK_USUARIO_1");

            entity.ToTable("USUARIO");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.DocumentoIdDocumento).HasColumnName("DOCUMENTO_idDocumento");
            entity.Property(e => e.EstadoIdEstado).HasColumnName("ESTADO_idEstado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numeroDocumento");
            entity.Property(e => e.RolIdRol).HasColumnName("ROL_idRol");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.DocumentoIdDocumentoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.DocumentoIdDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIO_DOCUMENTO");

            entity.HasOne(d => d.EstadoIdEstadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EstadoIdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIO_ESTADO");

            entity.HasOne(d => d.RolIdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolIdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIO_ROL");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
