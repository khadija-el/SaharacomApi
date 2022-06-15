using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace saharacomnew.Models
{
    public class DetailLivraison
    {
        [Key]
        public int id {get; set;}
        public int num {get; set;}
        public int qteup {get; set;}
        public double prixht {get; set;}
        public double Montantht {get; set;}
        public double prixttc {get; set;}
        public double montantttc {get; set;}
        [Required]
        [ForeignKey("Articles")]
        public int Articleid {get; set;}
        [Required]
        [ForeignKey("Livraisons")]
        public int Livraisonid {get; set;}
        [Required]
        [ForeignKey("Tvas")]
        public int Tvaid {get; set;}
    }
}