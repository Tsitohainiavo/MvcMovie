using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Exemple d'une entité
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Test>().ToTable("TESTS");
            modelBuilder.Entity<Produit>().ToTable("PRODUIT").Property(p => p.Prix).HasColumnName("PRIX");
            modelBuilder.Entity<Test>().ToTable("USER");
            modelBuilder.Entity<Utilisateur>().ToTable("UTILISATEURS");
            modelBuilder.Entity<Utilisateur>().Property(u => u.ID).HasColumnName("ID");
            modelBuilder.Entity<Utilisateur>().Property(u => u.EMAIL).HasColumnName("EMAIL");
            modelBuilder.Entity<Utilisateur>().Property(u => u.PASSWORD).HasColumnName("PASSWORD");
        }
    }
}