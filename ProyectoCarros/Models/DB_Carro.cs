namespace ProyectoCarros.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DB_Carro : DbContext
    {
        public DB_Carro()
            : base("name=DB_Carro")
        {
        }

        public virtual DbSet<Carro> Carro { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carro>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Carro>()
                .Property(e => e.modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Marca>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Marca>()
                .Property(e => e.nota)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.nota)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.nick)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.contrasena)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Venta)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.id_Cliente);

            modelBuilder.Entity<Venta>()
                .Property(e => e.fecha_Venta)
                .IsUnicode(false);
        }
    }
}
