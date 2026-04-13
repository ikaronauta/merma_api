// data/AppDbContext.cs

using Microsoft.EntityFrameworkCore;

using Merma_API.Models;

namespace Merma_API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<CoffeeStore> CoffeeStores { get; set; }
    public DbSet<Menu> Menus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // USER
        modelBuilder.Entity<User>(entity => 
        {
            entity.ToTable("JefeCadena");

            entity.HasKey(u => u.Id);

            entity.HasAlternateKey(u => u.Ruta);
        });

        // COFFEE STORE (Tokens)
        modelBuilder.Entity<CoffeeStore>(entity =>
        {
            entity.ToTable("Tokens");

            entity.HasKey(cs => cs.Id);

            entity.HasAlternateKey(cs => cs.IdTienda);

            entity.HasOne(cs => cs.User) // relacion uno a muchos
                .WithMany(u => u.CoffeeStores) // relacion uno a muchos
                .HasForeignKey(cs => cs.Ruta) // Define qué columna es la FK (clave foránea)
                .HasPrincipalKey(u => u.Ruta); // Indica a qué columna apunta la FK.
        });

        // MENU
        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menus");

            entity.HasKey(m => m.Id);

            // Relación por IdTienda (clave de negocio)
            entity.HasOne(m => m.CoffeeStore)
                .WithMany(cs => cs.Menus)
                .HasForeignKey(m => m.IdTienda)
                .HasPrincipalKey(cs => cs.IdTienda);

            entity.Property(m => m.StatusProd) 
                .HasConversion<string>(); // Convierte el enum Convierte el enum para que se pueda guardar como texto "Activo", "Inactivo" de lo contrario seria 0, 1
        });
    }

    // public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    // {

    // }
}