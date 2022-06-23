using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace saharacomnew.Models
{
    public class Tva
    {
        [Key]
        public int id {get; set;}
        public string? code {get; set;}
        public string? designation {get; set;}
        public int taux {get; set;}
        public virtual ICollection<DetailLivraisonClient>? DetailLivraisonClients { get; set; }

    }
}