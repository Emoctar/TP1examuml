using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using TP1examuml.Data;
using TP1examuml.Models;
using TP1examuml.Interface;

namespace TP1examuml.Controllers

{
    [Authorize]
    public class ConsultationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConsultationRepository _consultationRepository;

        public ConsultationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
          IConsultationRepository consultationRepository)
        {
            _context = context;
            _userManager = userManager;
            _consultationRepository= consultationRepository;
        }

        // GET: Consultations
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? page)
        {

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            if (User.IsInRole("Medecin"))
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                var Id = (int)user.MedecinId;
                var applicationDbContext = _context.Consultation.Include(c => c.Medecin).Include(c => c.Patient)
               .Include(c => c.TypeConsultation).Where(c => c.MedecinId == Id);

                return View(await applicationDbContext.ToPagedListAsync(pageNumber, 1));
            }
            else
            {
                var applicationDbContext = _context.Consultation.Include(c => c.Medecin).Include(c => c.Patient)
             .Include(c => c.TypeConsultation);
                return View(await applicationDbContext.ToPagedListAsync(pageNumber, 1));


            }



        }

        public async Task<IActionResult> VoirExam(int? id)

        {
            var donnees= _context.Examen.Include(c=>c.Analyse).Where(c => c.ConsultationId == id);
            return View(donnees);
        }

        // GET: Consultations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consultation == null)
            {
                return NotFound();
            }    

            var consultation = await _context.Consultation
                .Include(c => c.Medecin)
                .Include(c => c.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // GET: Consultations/Create
        [Authorize(Roles = "Medecin")]
        public IActionResult Create()
        {
           //ViewData["MedecinId"] = new SelectList(_context.Medecin, "Id", "DisplayName");
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "DisplayName");
            ViewData["TypeConsultationId"] = new SelectList(_context.TypeConsultation, "Id", "Nom");
            return View();
        }

        // POST: Consultations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Motif,Remarques,PatientId,MedecinId,TypeConsultationId")] Consultation consultation)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            consultation.MedecinId =(int)user.MedecinId;
            if (ModelState.IsValid)
            {
                consultation.DateConsultation= DateTime.Now;
                _context.Add(consultation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           // ViewData["MedecinId"] = new SelectList(_context.Medecin, "Id", "DisplayName", consultation.MedecinId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "DisplayName", consultation.PatientId);
            ViewData["TypeConsultationId"] = new SelectList(_context.Patient, "Id", "Nom", consultation.TypeConsultation);
            return View(consultation);
        }

        // GET: Consultations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consultation == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultation.FindAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }
            ViewData["MedecinId"] = new SelectList(_context.Medecin, "Id", "DisplayName", consultation.MedecinId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "DisplayName", consultation.PatientId);
            return View(consultation);
        }

        // POST: Consultations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Motif,Remarques,PatientId,MedecinId")] Consultation consultation)
        {
            if (id != consultation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultationExists(consultation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedecinId"] = new SelectList(_context.Medecin, "Id", "DisplayName", consultation.MedecinId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "DisplayName", consultation.PatientId);
            return View(consultation);
        }

        // GET: Consultations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consultation == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultation
                .Include(c => c.Medecin)
                .Include(c => c.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // POST: Consultations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consultation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Consultation'  is null.");
            }
            var consultation = await _context.Consultation.FindAsync(id);
            if (consultation != null)
            {
                _context.Consultation.Remove(consultation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultationExists(int id)
        {
          return _context.Consultation.Any(e => e.Id == id);
        }
    }
}
