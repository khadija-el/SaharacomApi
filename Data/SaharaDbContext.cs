using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using saharacomnew.Models;

namespace saharacomnew.Data
{
    public class SaharaDbContext : DbContext
    {
        public SaharaDbContext(DbContextOptions<SaharaDbContext> dbContextOptions) : base(dbContextOptions)
        {
            try{
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(databaseCreator != null)
                {
                    if(!databaseCreator.CanConnect()) databaseCreator.Create();
                    if(!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        public  virtual DbSet<Article> Articles {get; set;}
        public virtual DbSet<Client> Clients {get; set;}
        public virtual DbSet<DetailLivraisonClient> DetailLivraisonClient {get; set;}
        public virtual DbSet<LivraisonClient> LivraisonClient {get; set;}
        public virtual DbSet<Tva> Tva {get; set;}

          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.reference);
                entity.Property(e => e.designation);
                entity.Property(e => e.stockinitial);
                entity.Property(e => e.stockfinal);
                entity.Property(e => e.qteachete);
                entity.Property(e => e.qtevendue);
                entity.Property(e => e.prixachatHt);
                entity.Property(e => e.prixachatttc);
                entity.Property(e => e.prixventeHt);
                entity.Property(e => e.prixventettc);
                entity.Property(e => e.prixventettc);
                entity.Property(e => e.info);
                entity.HasMany(e => e.DetailLivraisonClients).WithOne(p => p.Article).HasForeignKey(e => e.IdArticle).OnDelete(DeleteBehavior.NoAction);
               
            });
             modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.raisonSocial);
                entity.Property(e => e.tel);
                entity.Property(e => e.adresse);
                entity.HasMany(e => e.LivraisonClients).WithOne(p => p.Client).HasForeignKey(e => e.IdClient).OnDelete(DeleteBehavior.NoAction);
                
            });
            modelBuilder.Entity<LivraisonClient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Numero);
                entity.Property(e => e.Date);
                entity.Property(e => e.Info);
                entity.Property(e => e.MontantHT);
                // entity.Property(e => e.Tva);
                entity.Property(e => e.MontantTTC);
                entity.Property(e => e.IdClient);
                entity.HasMany(e => e.DetailLivraisonClients).WithOne(p => p.LivraisonClient).HasForeignKey(e => e.IdLivraisonClient).OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<DetailLivraisonClient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();
                entity.Property(e => e.Numero);
                // entity.Property(e => e.Designation);
                entity.Property(e => e.Qte);
                entity.Property(e => e.PUVHT_Brut);
                entity.Property(e => e.MontantHT);
                entity.Property(e => e.PUVTTC_Brut);
                entity.Property(e => e.MontantTTC);
                // entity.Property(e => e.Tva);
                entity.Property(e => e.IdLivraisonClient);
                entity.Property(e => e.IdArticle);

            });

              modelBuilder.Entity<Tva>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.code);
                entity.Property(e => e.designation);
                entity.Property(e => e.taux);
                entity.HasMany(e => e.DetailLivraisonClients).WithOne(p => p.Tva).HasForeignKey(e => e.idTva).OnDelete(DeleteBehavior.NoAction);
                
            });
            
        
        }
    }
}