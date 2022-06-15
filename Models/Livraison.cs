using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace saharacomnew.Models
{
    public class Livraison
    {
        [Key]
        public int id {get; set;}
        public int num {get; set;}
        public DateTime date {get; set;}
        public string? info {get; set;}
        public double Montantht {get; set;}
        public int tva {get; set;}
        public double montantttc {get; set;}
        [Required]
        [ForeignKey("Clients")]
        public int clientid {get; set;}
    }
}