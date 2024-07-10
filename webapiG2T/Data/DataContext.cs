using G2T.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace G2T.Data
{
    public class DataContext : IdentityDbContext<Utilisateur>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<TypeService> TypeServices { get; set; }
        public DbSet<Canal> Canaux { get; set; }
        public DbSet<Motif> Motifs { get; set; }
        public DbSet<SousMotif> SousMotifs { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<EntiteEnCharge> EntiteEnCharges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();

            modelBuilder.Entity<Facture>()
                .HasKey(f => new { f.CompteId, f.ServiceId });
            modelBuilder.Entity<Facture>()
                .HasOne(p => p.Compte)
                .WithMany(pc => pc.Factures)
                .HasForeignKey(p => p.CompteId);
            modelBuilder.Entity<Facture>()
                .HasOne(p => p.Service)
                .WithMany(p => p.Factures)
                .HasForeignKey(p => p.ServiceId);
        }
    }
}
