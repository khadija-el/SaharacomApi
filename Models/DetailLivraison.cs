using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace saharacomnew.Models
{
    public class DetailLivraisonClient
    {
        [Key]
        public int Id {get; set;}
        public int Numero {get; set;}
        public int Qte {get; set;}
        public double PUVHT_Brut {get; set;}
        public double MontantHT {get; set;}
        public double PUVTTC_Brut {get; set;}
        public double MontantTTC {get; set;}
        [Required]
        [ForeignKey("Articles")]
       	public int IdArticle { get; set; }

        [Required]
        [ForeignKey("Livraisons")]
        public int IdLivraisonClient {get; set;}
        [Required]
        [ForeignKey("Tvas")]
        public int idTva {get; set;}
        public Tva? Tva { get; set; }

        public LivraisonClient? LivraisonClient { get; set; }
		public Article? Article { get; set; }

    }
}