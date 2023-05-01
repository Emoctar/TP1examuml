using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP1examuml.Models
{
    public class RendezVous
    {
        public int Id { get; set; }
        public DateTime DateRv { get; set; }
        [DataType(DataType.MultilineText)] // textarea
        public string Description { get; set; }

        [ForeignKey("MedecinId")]
        public int MedecinId { get; set; }

        [ForeignKey("PatientId")]
       
        public int PatientId { get; set; }
        public int Telephone { get;set; }

        public int Statut { get; set; }

        public virtual Medecin? Medecin { get; set; }
        public virtual Patient? Patient { get; set; }


      

    }
}
