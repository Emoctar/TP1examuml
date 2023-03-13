using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP1examuml.Data;
using TP1examuml.Models;

namespace TP1examuml.Controllers
{
    public class LaboratoiresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LaboratoiresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Laboratoires
        public async Task<IActionResult> Index()
        {
              return View(await _context.Laboratoire.ToListAsync());
        }

        // GET: Laboratoires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Laboratoire == null)
            {
                return NotFound();
            }

            var laboratoire = await _context.Laboratoire
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laboratoire == null)
            {
                return NotFound();
            }

            return View(laboratoire);
        }

        // GET: Laboratoires/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Laboratoires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Adresse,Numero,Email")] Laboratoire laboratoire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laboratoire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(laboratoire);
        }

        // GET: Laboratoires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Laboratoire == null)
            {
                return NotFound();
            }

            var laboratoire = await _context.Laboratoire.FindAsync(id);
            if (laboratoire == null)
            {
                return NotFound();
            }
            return View(laboratoire);
        }

        // POST: Laboratoires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Adresse,Numero,Email")] Laboratoire laboratoire)
        {
            if (id != laboratoire.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laboratoire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaboratoireExists(laboratoire.Id))
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
            return View(laboratoire);
        }

        // GET: Laboratoires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Laboratoire == null)
            {
                return NotFound();
            }

            var laboratoire = await _context.Laboratoire
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laboratoire == null)
            {
                return NotFound();
            }

            return View(laboratoire);
        }

        // POST: Laboratoires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Laboratoire == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Laboratoire'  is null.");
            }
            var laboratoire = await _context.Laboratoire.FindAsync(id);
            if (laboratoire != null)
            {
                _context.Laboratoire.Remove(laboratoire);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaboratoireExists(int id)
        {
          return _context.Laboratoire.Any(e => e.Id == id);
        }
    }
}
