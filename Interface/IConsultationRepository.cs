using TP1examuml.Models;
using X.PagedList;

namespace TP1examuml.Interface
{
    public interface IConsultationRepository
    {
        Task<List<Examen>> GetExamByConsultation(int id);

        Task<IPagedList<Consultation>> GetConsultation(int page, int nombre);
        Task<IPagedList<Consultation>> GetConsultation(int page, int nombre,int medecinId);

    }

}
