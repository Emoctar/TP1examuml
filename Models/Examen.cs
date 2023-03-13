using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP1examuml.Models
{
    public class Examen
    {
       public int Id { get; set; }
        [ForeignKey("Analyse")]
        [DisplayName("Analyse")]
        public int AnalyseId { get; set; }
        [ForeignKey("Consultation")]
        [DisplayName("Consutation")]
        public int ConsultationId { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Resultat { get; set; }
        public DateTime? DateExamen { get; set; }
        public DateTime? DateResultat { get; set; }

        public virtual Consultation? Consultation { get; set; }
        public virtual Analyse? Analyse { get; set; }



    }
}
