namespace MvcMovie.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Exemple d'une entité
    public DbSet<Produit> Produits { get; set; }

    public DbSet<Test> Tests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Test>().ToTable("TESTS");
        modelBuilder.Entity<Produit>().ToTable("PRODUIT").Property(p => p.Prix).HasColumnName("PRIX"); ;
    }
}
