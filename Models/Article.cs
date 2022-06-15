using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace saharacomnew.Models
{
    public class Article
    {
        [Key]
        public int id {get; set;}
        public string? refence {get; set;}
        public string? designation {get; set;}
        public int stockfinal {get; set;}
        public int stockinitial {get; set;}
        public int qtevendue {get; set;}
        public int qteacheté {get; set;}
        public double prixachatHt {get; set;}
        public double prixachatttc {get; set;}
        public double prixventeHt {get; set;}
        public double prixventettc {get; set;}
        public string? info {get; set;}
    }
}