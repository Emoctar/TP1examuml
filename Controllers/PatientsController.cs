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
using TP1examuml.Data;
using TP1examuml.Interface;
using TP1examuml.Models;

namespace TP1examuml.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISendSmsEmailRepository _sendSmsEmailRepository;

        public PatientsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            ISendSmsEmailRepository sendSmsEmailRepository)
        {
            _context = context;
            _userManager = userManager;
            _sendSmsEmailRepository= sendSmsEmailRepository;
        }

        // GET: Patients
        [Authorize(Roles = "Patient,Admin")]
        public async Task<IActionResult> Index()
        {

            
            return _context.Patient != null ?
                          View(await _context.Patient.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Patient'  is null.");
           
            

        }
        //Liste de medecins

        [Authorize("")]
        public async Task<IActionResult> ListeDesMedecins()
        {
            //ViewData["Medecin"] = new SelectList(await _context.Medecin.ToListAsync());
            //return View();
            var liste = await _context.Medecin.ToListAsync();
            return View(liste);
        }


        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patient == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Email,Profession")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                //Creation d un medecin
                _context.Add(patient);
                await _context.SaveChangesAsync();

                //creation dun utilisateur
                ApplicationUser applicationUser = new()
                {
                    Email = patient.Email,
                    UserName = patient.Email,
                    DisplayName = patient.DisplayName,
                    MedecinId = patient.Id,


                };
                //

                var result = await _userManager.CreateAsync(applicationUser, "Passer@123");
                if (result.Succeeded)
                {
                    //Ajout du Role a l'utilisateur
                    await _userManager.AddToRoleAsync(applicationUser, "patient");
                }
                await _sendSmsEmailRepository.SendSmsAsync(" Votre compte a ete creer avec succes ");
                return RedirectToAction(nameof(Index));
            }
          
            return View(patient);
        }
            
        

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patient == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Email,Profession")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patient == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patient == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Patient'  is null.");
            }
            var patient = await _context.Patient.FindAsync(id);
            if (patient != null)
            {
                _context.Patient.Remove(patient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
          return (_context.Patient?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
