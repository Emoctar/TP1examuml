using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP1examuml.Data;
using TP1examuml.Models;

namespace TP1examuml.Controllers
{
    public class ExamenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Examen
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Examen.Include(e => e.Analyse).Include(e => e.Consultation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Examen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Examen == null)
            {
                return NotFound();
            }

            var examen = await _context.Examen
                .Include(e => e.Analyse)
                .Include(e => e.Consultation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examen == null)
            {
                return NotFound();
            }

            return View(examen);
        }

        // GET: Examen/Create
        public IActionResult Create()
        {
            ViewData["AnalyseId"] = new SelectList(_context.Analyse, "Id", "Id");
            ViewData["ConsultationId"] = new SelectList(_context.Consultation, "Id", "Motif");
            return View();
        }

        // POST: Examen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnalyseId,ConsultationId,Resultat,DateExamen,DateResultat")] Examen examen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnalyseId"] = new SelectList(_context.Analyse, "Id", "Id", examen.AnalyseId);
            ViewData["ConsultationId"] = new SelectList(_context.Consultation, "Id", "Motif", examen.ConsultationId);
            return View(examen);
        }

        // GET: Examen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Examen == null)
            {
                return NotFound();
            }

            var examen = await _context.Examen.FindAsync(id);
            if (examen == null)
            {
                return NotFound();
            }
            ViewData["AnalyseId"] = new SelectList(_context.Analyse, "Id", "Id", examen.AnalyseId);
            ViewData["ConsultationId"] = new SelectList(_context.Consultation, "Id", "Motif", examen.ConsultationId);
            return View(examen);
        }

        // POST: Examen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnalyseId,ConsultationId,Resultat,DateExamen,DateResultat")] Examen examen)
        {
            if (id != examen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamenExists(examen.Id))
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
            ViewData["AnalyseId"] = new SelectList(_context.Analyse, "Id", "Id", examen.AnalyseId);
            ViewData["ConsultationId"] = new SelectList(_context.Consultation, "Id", "Motif", examen.ConsultationId);
            return View(examen);
        }

        // GET: Examen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Examen == null)
            {
                return NotFound();
            }

            var examen = await _context.Examen
                .Include(e => e.Analyse)
                .Include(e => e.Consultation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examen == null)
            {
                return NotFound();
            }

            return View(examen);
        }

        // POST: Examen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Examen == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Examen'  is null.");
            }
            var examen = await _context.Examen.FindAsync(id);
            if (examen != null)
            {
                _context.Examen.Remove(examen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamenExists(int id)
        {
          return _context.Examen.Any(e => e.Id == id);
        }
    }
}
