using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace saharacomnew.Models
{
    public class Client
    {
        [Key]
        public int id {get; set;}
        public string? email {get; set;}
        public string? raisonSocial {get; set;}
        public string? tel {get; set;}
        public string? adresse {get; set;}
    }
}