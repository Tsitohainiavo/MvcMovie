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
        modelBuilder.Entity<Test>().ToTable("TESTS");
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Produit>().ToTable("produit");
        base.OnModelCreating(modelBuilder);
    }
}
