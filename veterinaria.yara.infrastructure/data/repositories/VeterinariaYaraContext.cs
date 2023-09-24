
using Microsoft.EntityFrameworkCore;
using veterinaria.yara.domain.entities;

namespace veterinaria.yara.infrastructure.data.repositories
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Mascota> Mascotas { get; set; } = null!;
        public virtual DbSet<Mensaje> Mensajes { get; set; } = null!;
        public virtual DbSet<Raza> Razas { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuarioMascota> UsuarioMascotas { get; set; } = null!;
        public virtual DbSet<UsuarioRole> UsuarioRoles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__Estados__FBB0EDC178D786E5");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mascota>(entity =>
            {
                entity.HasKey(e => e.IdMascota)
                    .HasName("PK__Mascotas__5C9C26F05A2AC47C");

                entity.Property(e => e.IdMascota).ValueGeneratedNever();

                entity.Property(e => e.Mote)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Peso).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.Mascota)
                    .HasForeignKey(d => d.Estado)
                    .HasConstraintName("FK_Estados_IdEstado");

                entity.HasOne(d => d.IdRazaNavigation)
                    .WithMany(p => p.Mascota)
                    .HasForeignKey(d => d.IdRaza)
                    .HasConstraintName("FK__Mascotas__IdRaza__4316F928");
            });

            modelBuilder.Entity<Mensaje>(entity =>
            {
                entity.HasOne(d => d.Destinatario)
                    .WithMany(p => p.MensajeDestinatarios)
                    .HasForeignKey(d => d.DestinatarioId)
                    .HasConstraintName("FK__Mensajes__Destin__7F2BE32F");

                entity.HasOne(d => d.Remitente)
                    .WithMany(p => p.MensajeRemitentes)
                    .HasForeignKey(d => d.RemitenteId)
                    .HasConstraintName("FK__Mensajes__Remite__7E37BEF6");
            });

            modelBuilder.Entity<Raza>(entity =>
            {
                entity.HasKey(e => e.IdRaza)
                    .HasName("PK__Razas__8F06EB287100EC54");

                entity.Property(e => e.IdRaza).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Roles__2A49584CF81D87ED");

                entity.Property(e => e.IdRol).ValueGeneratedNever();

                entity.Property(e => e.NombreRol)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuarios__5B65BF977B3E4E1E");

                entity.Property(e => e.IdUsuario).ValueGeneratedNever();

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Clave)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Estado)
                    .HasConstraintName("PK_Estados_IdEstado");
            });

            modelBuilder.Entity<UsuarioMascota>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioMascota)
                    .HasName("PK__UsuarioM__5F18CAD19D6CDC6F");

                entity.Property(e => e.IdUsuarioMascota).ValueGeneratedNever();

                entity.HasOne(d => d.IdMascotaNavigation)
                    .WithMany(p => p.UsuarioMascota)
                    .HasForeignKey(d => d.IdMascota)
                    .HasConstraintName("FK__UsuarioMa__IdMas__4AB81AF0");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioMascota)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__UsuarioMa__IdUsu__49C3F6B7");
            });

            modelBuilder.Entity<UsuarioRole>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioRol)
                    .HasName("PK__UsuarioR__6806BF4A1434382C");

                entity.Property(e => e.IdUsuarioRol).ValueGeneratedNever();

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.UsuarioRoles)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__UsuarioRo__IdRol__46E78A0C");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioRoles)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__UsuarioRo__IdUsu__45F365D3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
