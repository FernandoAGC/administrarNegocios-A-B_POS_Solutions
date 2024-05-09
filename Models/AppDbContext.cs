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
               entity.HasIndex(i => i.Correo).IsUnique();
               entity.HasOne(o => o.TipoUsuario);
            });
                
            modelBuilder.Entity<TipoUsuario>(entity => {
               entity.HasKey(k => k.Id);
               entity.HasIndex(i => i.Nombre).IsUnique();
            });

            modelBuilder.Entity<Negocio>(entity => {
               entity.HasKey(k => k.Id);
               entity.HasIndex(i => i.Nombre).IsUnique();
               entity.Property(p => p.Descripcion).IsRequired(false);
               entity.HasOne(o => o.Usuario);
            });

            modelBuilder.Entity<CategoriaItem>(entity => {
               entity.HasKey(k => k.Id);
               entity.HasIndex(i => i.Nombre).IsUnique();
            });

            modelBuilder.Entity<Item>(entity => {
               entity.HasKey(k => k.Id);
               entity.Property(p => p.Precio).HasPrecision(15, 2);
               entity.Property(p => p.Descripcion).IsRequired(false);
               entity.HasOne(o => o.CategoriaItem);
               entity.HasOne(o => o.Negocio);
            });
        }
    }
}