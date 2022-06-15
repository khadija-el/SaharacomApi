using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace saharacomnew.Models
{
    public class Client
    {
        [Key]
        public int id {get; set;}
        public string? code {get; set;}
        public string? nom {get; set;}
        public string? numphone {get; set;}
        public string? adresse {get; set;}
    }
}