using Microsoft.EntityFrameworkCore;

namespace administrarNegocios_A_B_POS_Solutions.Models
{
    public partial class AppDbContext : DbContext
    {
        // Colecciones de objetos de cada entidad para mapear las tablas en la base de datos
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<TipoUsuario> TiposUsuario { get; set; }
        public virtual DbSet<Negocio> Negocios { get; set; }
        public virtual DbSet<CategoriaItem> CategoriasItem { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        
        // Se inicializa el context recibiendo la configuración de la conexión a la base de datos
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Configuracion del Estructura de base de datos CODE FIRST
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeo de cada entidad en tablas y relaciones
            modelBuilder.Entity<Usuario>(entity => {
               entity.HasKey(k => k.Id);
               entity.Property(p => p.Id).ValueGeneratedOnAdd();
               entity.HasIndex(i => i.Correo).IsUnique();
               entity.HasOne(o => o.TipoUsuario);
            });
                
            modelBuilder.Entity<TipoUsuario>(entity => {
               entity.HasKey(k => k.Id);
               entity.Property(p => p.Id).ValueGeneratedOnAdd();
               entity.HasIndex(i => i.Nombre).IsUnique();
            });

            modelBuilder.Entity<Negocio>(entity => {
               entity.HasKey(k => k.Id);
               entity.Property(p => p.Id).ValueGeneratedOnAdd();
               entity.HasIndex(i => i.Nombre).IsUnique();
               entity.Property(p => p.Descripcion).IsRequired(false);
               entity.HasOne(o => o.Usuario);
               entity.HasIndex(i => i.UsuarioId).IsUnique();
            });

            modelBuilder.Entity<CategoriaItem>(entity => {
               entity.HasKey(k => k.Id);
               entity.Property(p => p.Id).ValueGeneratedOnAdd();
               entity.HasIndex(i => i.Nombre).IsUnique();
            });

            modelBuilder.Entity<Item>(entity => {
               entity.HasKey(k => k.Id);
               entity.Property(p => p.Id).ValueGeneratedOnAdd();
               entity.Property(p => p.Precio).HasPrecision(15, 2);
               entity.Property(p => p.Descripcion).IsRequired(false);
               entity.HasOne(o => o.CategoriaItem);
               entity.HasOne(o => o.Negocio);
            });

            // Creacion de registros inciales para la base de datos

            modelBuilder.Entity<TipoUsuario>().HasData(
               new TipoUsuario { Id = 1, Nombre = "Administrador" },
               new TipoUsuario { Id = 2, Nombre = "Cliente" }
            );

            modelBuilder.Entity<Usuario>().HasData(
               new Usuario { Id = 1, Nombres = "Luis", Apellidos = "Guzmán", TipoUsuarioId = 1, Correo = "lg@gmail.com", Contraseña = "lg123"},
               new Usuario { Id = 2, Nombres = "Juan", Apellidos = "Peralta", TipoUsuarioId = 1, Correo = "jp@gmail.com", Contraseña = "jp123"},
               new Usuario { Id = 3, Nombres = "Pedro", Apellidos = "Garcia", TipoUsuarioId = 2, Correo = "pg@gmail.com", Contraseña = "pg123"},
               new Usuario { Id = 4, Nombres = "Leonardo", Apellidos = "Lima", TipoUsuarioId = 2, Correo = "ll@gmail.com", Contraseña = "ll123"},
               new Usuario { Id = 5, Nombres = "Javier", Apellidos = "Garcia", TipoUsuarioId = 2, Correo = "jg@gmail.com", Contraseña = "jg123"},
               new Usuario { Id = 6, Nombres = "Pablo", Apellidos = "Torres", TipoUsuarioId = 2, Correo = "pt@gmail.com", Contraseña = "pt123"}
            );

            modelBuilder.Entity<Negocio>().HasData(
               new Negocio { Id = 1, Nombre = "NegocioABC", Descripcion = "Negocio dedicado...", FechaCreacion = new DateTime(2022, 5, 10), UsuarioId = 3 },
               new Negocio { Id = 2, Nombre = "Negocio123", Descripcion = "Negocio con el fin de...", FechaCreacion = new DateTime(2020, 1, 15), UsuarioId = 4 },
               new Negocio { Id = 3, Nombre = "Negocio654", Descripcion = "Ofrecemos...", FechaCreacion = new DateTime(2018, 10, 3), UsuarioId = 5 }
            );

            modelBuilder.Entity<CategoriaItem>().HasData(
               new CategoriaItem { Id = 1, Nombre = "Categoria A" },
               new CategoriaItem { Id = 2, Nombre = "Categoria B" },
               new CategoriaItem { Id = 3, Nombre = "Categoria C" }
            );

            modelBuilder.Entity<Item>().HasData(
               new Item { Id = 1, Nombre = "Item A", Descripcion = "Descripcion itemA...", CategoriaItemId = 1, Precio = 3.45m, NegocioId = 1 },
               new Item { Id = 2, Nombre = "Item B", Descripcion = "Descripcion itemB...", CategoriaItemId = 2, Precio = 19.99m, NegocioId = 2 },
               new Item { Id = 3, Nombre = "Item C", Descripcion = "Descripcion itemC...", CategoriaItemId = 3, Precio = 8.00m, NegocioId = 3 }
            );
        }
    }
}