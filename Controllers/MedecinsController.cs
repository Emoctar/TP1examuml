using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP1examuml.Data;
using TP1examuml.Interface;
using TP1examuml.Models;

namespace TP1examuml.Controllers
{
    // [Authorize]
    public class MedecinsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISendSmsEmailRepository _sendSmsEmailRepository;

       
        public MedecinsController(ApplicationDbContext context ,UserManager<ApplicationUser> userManager,
            ISendSmsEmailRepository sendSmsEmail)
        {
            _context = context;
            _userManager = userManager;
            _sendSmsEmailRepository= sendSmsEmail;
        }

        // GET: Medecins 
        public async Task<IActionResult> Index(string? search)
        {
            //Controller la recherche d'element recherche
            if (string.IsNullOrEmpty(search) )
              return View(await _context.Medecin.ToListAsync());
            else
                return View(await _context.Medecin.
                    Where(m=>EF.Functions.Like(m.Prenom, "%" + search + "%"))
                    .ToListAsync());
        }
        [Authorize(Roles = "Medecin, Admin")]
        public async Task<IActionResult> ListeConsultationByMedecin(int? id)
        {
            var medecin = await _context.Medecin.Include(t=>t.Consultations)
               .FirstOrDefaultAsync(m => m.Id == id);
            return View(medecin);
        }
         [Authorize(Roles = "Medecin, Admin")]
        public async Task<IActionResult> ListeRendezVous(int? id)
        {
            //var rendezvous = await _context.Medecin.Include(m => m.rendez_vous)
            //   .FirstOrDefaultAsync(m => m.Id == id);
             var rendezvous = await _context.Medecin.Include(m => m.rendez_vous)
               .FirstOrDefaultAsync(m => m.Id == id);
            return View(rendezvous);
        }

        // Status rendez vous
        //public async Task<IActionResult> Attente(int? id)
        //{

        //    var attente = await _context.RendezVous.Include(r => r.RendezVous).Where(r => r.Statut==0).
        //       ToListAsync();
        //    return View(attente);

        //}
        //public async Task<IActionResult> Confirme(int? id)
        //{

        //    var confirme = await _context.RendezVous.Include(r => r.Medecin).Include(r => r.Patient).Where(r => r.Statut == 1).
        //       ToListAsync();
        //    return View(confirme);


        //}
        //public async Task<IActionResult> Annule(int? id)
        //{

        //    var Annule = await _context.RendezVous.Include(r => r.Medecin).Include(r => r.Patient).Where(r => r.Statut == 2).
        //       ToListAsync();
        //    return View(Annule);


        //}




        // GET: Medecins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medecin == null)
            {
                return NotFound();
            }

            var medecin = await _context.Medecin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medecin == null)
            {
                return NotFound();
            }

            return View(medecin);
        }

        // GET: Medecins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medecins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Email,Adresse,Specialite")] Medecin medecin)
        {
            if (ModelState.IsValid)
            { 
                //Creation d un medecin
                _context.Add(medecin);
                await _context.SaveChangesAsync();
                

                //creation dun utilisateur
                ApplicationUser applicationUser = new() {
                    Email = medecin.Email,
                    UserName = medecin.Email,
                    DisplayName = medecin.DisplayName,
                    MedecinId = medecin.Id,

                   
            };
                //

                var result = await _userManager.CreateAsync(applicationUser, "Passer@123");
                if (result.Succeeded)
                {
                    //Ajout du Role a l'utilisateur
                    await _userManager.AddToRoleAsync(applicationUser, "medecin");
                }
                await _sendSmsEmailRepository.SendSmsAsync(" Votre compte a ete creer avec succes ");
                return RedirectToAction(nameof(Index));

            }
           
            return View(medecin);
        }

        // GET: Medecins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medecin == null)
            {
                return NotFound();
            }

            var medecin = await _context.Medecin.FindAsync(id);
            if (medecin == null)
            {
                return NotFound();
            }
            return View(medecin);
        }

        // POST: Medecins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Email,Adresse,Specialite")] Medecin medecin)
        {
            if (id != medecin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medecin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedecinExists(medecin.Id))
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
            return View(medecin);
        }

        // GET: Medecins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medecin == null)
            {
                return NotFound();
            }

            var medecin = await _context.Medecin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medecin == null)
            {
                return NotFound();
            }

            return View(medecin);
        }

        // POST: Medecins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medecin == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Medecin'  is null.");
            }
            var medecin = await _context.Medecin.FindAsync(id);
            if (medecin != null)
            {
                _context.Medecin.Remove(medecin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedecinExists(int id)
        {
          return _context.Medecin.Any(e => e.Id == id);
        }
    }
}
