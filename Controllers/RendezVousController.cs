using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP1examuml.Data;
using TP1examuml.Data.Migrations;
using TP1examuml.Interface;
using TP1examuml.Models;

namespace TP1examuml.Controllers
{   
    public class RendezVousController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISendSmsEmailRepository _sendSmsEmailRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public RendezVousController(ApplicationDbContext context, ISendSmsEmailRepository sendSmsEmailRepository, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _sendSmsEmailRepository = sendSmsEmailRepository;
            _userManager = userManager;
        }

        // GET: RendezVous
        [Authorize(Roles ="Medecin,Admin")]
        public async Task<IActionResult> Index(int? id)
        {

            var med = await _context.RendezVous.Include(r => r.Medecin).Include(r => r.Patient).
               ToListAsync();
            return View(med);

        }
       


        // GET: RendezVous/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RendezVous == null)
            {
                return NotFound();
            }

            var rendezVous = await _context.RendezVous
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rendezVous == null)
            {
                return NotFound();
            }

            return View(rendezVous);
        }

        // GET: RendezVous/Create
        public IActionResult Create()
        {
            ViewData["MedecinId"] = new SelectList(_context.Medecin, "Id", "DisplayName");
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "DisplayName");
            return View();
        }

        // POST: RendezVous/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateRv,Description,PatientId,MedecinId,Telephone")] RendezVous rendezVous)
        {

            if (ModelState.IsValid)
            {
                _context.Add(rendezVous);
                await _context.SaveChangesAsync();
               
               /* return RedirectToAction(nameof(Home))*/;
            }
            await _sendSmsEmailRepository.SendSmsAsync("Requete transmise avec succes \n Patienter la reponse du Medecin ");
            return View(rendezVous);
        }

        [Authorize(Roles = "Medecin,Admin")]
        // GET: RendezVous/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null || _context.RendezVous == null)
            {
                return NotFound();
            }

            var rendezVous = await _context.RendezVous.FindAsync(id);
            if (rendezVous == null)
            {
                return NotFound();
            }
            return View(rendezVous);



        }
        [Authorize(Roles = "Medecin,Admin")]

        // POST: RendezVous/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedecinId,PatientId,Description,DateRv,Statut")] RendezVous rendezVous)
        {
            if (id != rendezVous.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rendezVous);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RendezVousExists(rendezVous.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (rendezVous.Statut == 1)
                {
                    var message = "Votre rendez-vous avec le Dr " + rendezVous.MedecinId + " prevu " + rendezVous.DateRv.ToString("dd/MM/yyyy") + " a été confirmé.";

                    await _sendSmsEmailRepository.SendSmsAsync(message);

                }
                if (rendezVous.Statut == 2)
                {
                    var message = "Votre rendez-vous avec le Dr " + rendezVous.MedecinId + " prevu " + rendezVous.DateRv.ToString("dd/MM/yyyy") + " a été annule.";

                    await _sendSmsEmailRepository.SendSmsAsync(message);
                }
                if (rendezVous.Statut == 0)
                {
                    var message = "Traitement En Cours \n Votre requete a ete envoye avec succes!!";

                    await _sendSmsEmailRepository.SendSmsAsync(message);
                }

                _context.Update(rendezVous);

                await _context.SaveChangesAsync();

            }
           

           



            return RedirectToAction(nameof(Index));
        }

        // GET: RendezVous/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RendezVous == null)
            {
                return NotFound();
            }

            var rendezVous = await _context.RendezVous
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rendezVous == null)
            {
                return NotFound();
            }

            return View(rendezVous);
        }

        // POST: RendezVous/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RendezVous == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RendezVous'  is null.");
            }
            var rendezVous = await _context.RendezVous.FindAsync(id);
            if (rendezVous != null)
            {
                _context.RendezVous.Remove(rendezVous);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RendezVousExists(int id)
        {
          return (_context.RendezVous?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
