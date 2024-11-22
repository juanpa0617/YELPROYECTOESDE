using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YELPROYECTOESDE.Data;
using YELPROYECTOESDE.Models;

namespace YELPROYECTOESDE.Controllers
{
    public class ComodidadsController : Controller
    {
        private readonly AlojamientoDbContext _context;

        public ComodidadsController(AlojamientoDbContext context)
        {
            _context = context;
        }

        // GET: Comodidads
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comodidades.ToListAsync());
        }

        // GET: Comodidads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comodidad = await _context.Comodidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comodidad == null)
            {
                return NotFound();
            }

            return View(comodidad);
        }

        // GET: Comodidads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comodidads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Estado")] Comodidad comodidad)
        {
            
                _context.Add(comodidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Comodidads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comodidad = await _context.Comodidades.FindAsync(id);
            if (comodidad == null)
            {
                return NotFound();
            }
            return View(comodidad);
        }

        // POST: Comodidads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Estado")] Comodidad comodidad)
        {
            if (id != comodidad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comodidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComodidadExists(comodidad.Id))
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
            return View(comodidad);
        }

        // GET: Comodidads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comodidad = await _context.Comodidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comodidad == null)
            {
                return NotFound();
            }

            return View(comodidad);
        }

        // POST: Comodidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comodidad = await _context.Comodidades.FindAsync(id);
            if (comodidad != null)
            {
                _context.Comodidades.Remove(comodidad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComodidadExists(int id)
        {
            return _context.Comodidades.Any(e => e.Id == id);
        }
    }
}
