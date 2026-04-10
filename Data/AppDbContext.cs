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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity => 
        {
            entity.ToTable("JefeCadena");

            entity.HasKey(u => u.Id);
        });

        modelBuilder.Entity<CoffeeStore>(entity =>
        {
            entity.ToTable("Tokens");

            entity.HasOne(cs => cs.User)
                .WithMany(u => u.CoffeeStores)
                .HasForeignKey(cs => cs.Ruta)   // FK real
                .HasPrincipalKey(u => u.Ruta);      // relación por Ruta
        });
    }

    // public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    // {

    // }
}