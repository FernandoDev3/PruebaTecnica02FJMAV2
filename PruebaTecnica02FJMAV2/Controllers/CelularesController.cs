using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PruebaTecnica02FJMAV2.Models;

namespace PruebaTecnica02FJMAV2.Controllers
{
    public class CelularesController : Controller
    {
        private readonly PruebaTecnica02FJMAV2Context _context;

        public CelularesController(PruebaTecnica02FJMAV2Context context)
        {
            _context = context;
        }

        // GET: Celulares
        public async Task<IActionResult> Index()
        {
            var pruebaTecnica02FJMAV2Context = _context.Celulares.Include(c => c.Marca);
            return View(await pruebaTecnica02FJMAV2Context.ToListAsync());
        }

        // GET: Celulares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Celulares == null)
            {
                return NotFound();
            }

            var celulare = await _context.Celulares
                .Include(c => c.Marca)
                .FirstOrDefaultAsync(m => m.CelularId == id);
            if (celulare == null)
            {
                return NotFound();
            }

            return View(celulare);
        }

        // GET: Celulares/Create
        public IActionResult Create()
        {
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "Nombre");
            return View();
        }

        // POST: Celulares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CelularId,Nombre,Precio,Descripcion,MarcaId")] Celulare celulare, IFormFile imagen)
        {
            if (imagen != null && imagen.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    celulare.Imagen = memoryStream.ToArray();
                }
            }
            _context.Add(celulare);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{
                
                
            //}
            //ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "Nombre", celulare.MarcaId);
            //return View(celulare);
        }

        // GET: Celulares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Celulares == null)
            {
                return NotFound();
            }

            var celulare = await _context.Celulares.FindAsync(id);
            if (celulare == null)
            {
                return NotFound();
            }
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "Nombre", celulare.MarcaId);
            return View(celulare);
        }

        // POST: Celulares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CelularId,Nombre,Precio,Descripcion,MarcaId")] Celulare celulare, IFormFile imagen)
        {
            if (id != celulare.CelularId)
            {
                return NotFound();
            }
            if (imagen != null && imagen.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    celulare.Imagen = memoryStream.ToArray();
                }
                _context.Update(celulare);
                await _context.SaveChangesAsync();
            }
            else
            {
                var celularFin = await _context.Celulares.FirstOrDefaultAsync(s=> s.CelularId == celulare.CelularId);
                if(celularFin?.Imagen?.Length > 0)
                celulare.Imagen = celularFin.Imagen;
                celularFin.Nombre = celulare.Nombre;
                celularFin.Precio = celulare.Precio;
                celularFin.Descripcion = celulare.Descripcion;
                celularFin.MarcaId = celulare.MarcaId;
                _context.Update(celularFin);
                await _context.SaveChangesAsync();
            }
            try
            {
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CelulareExists(celulare.CelularId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            //if (ModelState.IsValid)
            //{

            //}
            //ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "Nombre", celulare.MarcaId);
            //return View(celulare);
        }

        // GET: Celulares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Celulares == null)
            {
                return NotFound();
            }

            var celulare = await _context.Celulares
                .Include(c => c.Marca)
                .FirstOrDefaultAsync(m => m.CelularId == id);
            if (celulare == null)
            {
                return NotFound();
            }

            return View(celulare);
        }

        // POST: Celulares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Celulares == null)
            {
                return Problem("Entity set 'PruebaTecnica02FJMAV2Context.Celulares'  is null.");
            }
            var celulare = await _context.Celulares.FindAsync(id);
            if (celulare != null)
            {
                _context.Celulares.Remove(celulare);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteImagen(int? id)
        {
            var celularFin = await _context.Celulares.FirstOrDefaultAsync(s => s.CelularId == id);
            celularFin.Imagen = null;
            _context.Update(celularFin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CelulareExists(int id)
        {
          return (_context.Celulares?.Any(e => e.CelularId == id)).GetValueOrDefault();
        }
    }
}
