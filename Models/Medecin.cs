using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP1examuml.Models
{
    public class Medecin
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public string Specialite { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }


        public virtual List<RendezVous>? rendez_vous { get; set; }
      
        public virtual List<Consultation>? Consultations { get; set; }
        [NotMapped]
        public string DisplayName { get => Prenom + " " + Nom; }

    }

    public class Patient
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Profession { get; set; }
        public virtual List<Consultation>? Consultations { get; set; }
        public virtual List<Medecin>?Medecins  { get; set; }
        [NotMapped]
        public string DisplayName { get => Prenom + " " + Nom; }
 


    }

    public class Consultation
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Motif Consultation")]
        public string Motif { get; set; }
        [Required]
        [DisplayName("Les remarques du medecin")]
        [DataType(DataType.MultilineText)] // textarea
        public string Remarques { get; set; }

        [ForeignKey("Patient")]
        [DisplayName("Le patient")]
        public int PatientId { get; set; }

        public virtual Patient? Patient { get; set; }

        [ForeignKey("Medecin")]
        [DisplayName("Le medecin")]
        public int MedecinId { set; get; }
        public virtual Medecin? Medecin { get; set; }
        [Column("date_consultation")]
        [DisplayFormat(DataFormatString ="{0:dd MMM yyyy}")]
        public DateTime? DateConsultation { get; set; }

        [ForeignKey("TypeConsultation")]
        [DisplayName("Type Consultation")]
        public int TypeConsultationId { get; set; }
        public virtual TypeConsultation? TypeConsultation { get; set; }

       

    }
}