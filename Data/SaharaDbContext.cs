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
        public virtual DbSet<DetailLivraison> DetailLivraisons {get; set;}
        public virtual DbSet<Livraison> Livraisons {get; set;}
        public virtual DbSet<Tva> Tvas {get; set;}

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
               
            });
        }
    }
}