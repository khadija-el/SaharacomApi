using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace saharacomnew.Models
{
    public class LivraisonClient
    {
        [Key]
        public int Id {get; set;}
        public int Numero {get; set;}
        public DateTime Date {get; set;}
        public string? Info {get; set;}
        public double MontantHT {get; set;}
        public int tva {get; set;}
        public double MontantTTC {get; set;}
        [Required]
        [ForeignKey("Clients")]
        public int IdClient {get; set;}
        public Client? Client { get; set; }

		public virtual ICollection<DetailLivraisonClient>? DetailLivraisonClients { get; set; }

		
    }
}