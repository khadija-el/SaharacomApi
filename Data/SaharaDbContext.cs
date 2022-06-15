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
    }
}