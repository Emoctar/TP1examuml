using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP1examuml.Data;
using TP1examuml.Models;

namespace TP1examuml.Controllers
{
    public class AnalysesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnalysesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Analyses
        public async Task<IActionResult> Index()
        {
              return View(await _context.Analyse.ToListAsync());
        }

        // GET: Analyses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Analyse == null)
            {
                return NotFound();
            }

            var analyse = await _context.Analyse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (analyse == null)
            {
                return NotFound();
            }

            return View(analyse);
        }

        // GET: Analyses/Create

        public IActionResult Create()
        {
            ViewData["LaboratoireId"] = new SelectList(_context.Laboratoire, "Id", "Nom");
            return View();
        }

        // POST: Analyses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prix,LaboratoireId")] Analyse analyse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(analyse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(analyse);
        }

        // GET: Analyses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Analyse == null)
            {
                return NotFound();
            }

            var analyse = await _context.Analyse.FindAsync(id);
            if (analyse == null)
            {
                return NotFound();
            }
            return View(analyse);
        }

        // POST: Analyses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prix,LaboratoireId")] Analyse analyse)
        {
            if (id != analyse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(analyse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalyseExists(analyse.Id))
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
            return View(analyse);
        }

        // GET: Analyses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Analyse == null)
            {
                return NotFound();
            }

            var analyse = await _context.Analyse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (analyse == null)
            {
                return NotFound();
            }

            return View(analyse);
        }

        // POST: Analyses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Analyse == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Analyse'  is null.");
            }
            var analyse = await _context.Analyse.FindAsync(id);
            if (analyse != null)
            {
                _context.Analyse.Remove(analyse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnalyseExists(int id)
        {
          return _context.Analyse.Any(e => e.Id == id);
        }
    }
}
