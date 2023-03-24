using Microsoft.EntityFrameworkCore;
using TP1examuml.Data;
using TP1examuml.Interface;
using TP1examuml.Models;
using X.PagedList;

namespace TP1examuml.Service
{
    public class ConsultationRepository : IGeneric<Consultation>, IConsultationRepository
    {
        private readonly ApplicationDbContext _context;
        public ConsultationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Consultation entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Consultation>> GEtAllAsync()
        {
           return  await _context.Consultation.Include(c => c.Medecin).Include(c => c.Patient)
             .Include(c => c.TypeConsultation).ToListAsync();

        }

        public async Task<Consultation> GetBYId(int id)
        {
            
            return await _context.Consultation.Include(c => c.Medecin).Include(c => c.Patient)
             .Include(c => c.TypeConsultation).FirstOrDefaultAsync(t=>t.Id==id);
        }

        public async Task<IPagedList<Consultation>> GetConsultation(int page, int nombre)
        {
            return await _context.Consultation.Include(c => c.Medecin).Include(c => c.Patient)
              .Include(c => c.TypeConsultation).ToPagedListAsync(page,nombre);
        }

        public async Task<IPagedList<Consultation>> GetConsultation(int page, int nombre, int medecinId)
        {
            return await _context.Consultation.Include(c => c.Medecin).Include(c => c.Patient)
                .Where(c=>c.MedecinId==medecinId)
            .Include(c => c.TypeConsultation).ToPagedListAsync(page, nombre);
        }

        public Task<List<Examen>> GetExamByConsultation(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Consultation entity)
        {
            throw new NotImplementedException();
        }
    }
}
