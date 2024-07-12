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

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(i => new { i.LoginProvider, i.ProviderKey });
                entity.HasOne<Utilisateur>().WithMany().HasForeignKey(i => i.UserId);
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(i => new { i.UserId, i.RoleId });
                entity.HasOne<IdentityRole>().WithMany().HasForeignKey(i => i.RoleId);
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(i => new { i.UserId, i.LoginProvider, i.Name });
            });

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
