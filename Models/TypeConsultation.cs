using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP1examuml.Models
{
    public class TypeConsultation
    {
        public int Id { get; set; }
        [DisplayName("Type Consultation")]
        public string Nom { get; set; }
        public string Description { get; set; }
        public virtual List<TypeConsultation>? TypeConsultations { get; set;}
        
    }

    public class Analyse
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int Prix { get; set; }
        [ForeignKey("Laboratoire")]
        [DisplayName("Labo")]
        public int LaboratoireId { get; set; }
       // public virtual List<Consultation>? Consultations { get; set; }

    }

    public class Laboratoire
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public int Numero { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public virtual List<Analyse>?   Analyse { get; set;}

      
    }

}
