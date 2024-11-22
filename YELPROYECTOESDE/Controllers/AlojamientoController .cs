﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YELPROYECTOESDE.Data;
using YELPROYECTOESDE.Models;

public class AlojamientoController : Controller
{
    private readonly AlojamientoDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AlojamientoController(AlojamientoDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    // GET: Alojamiento
    public async Task<IActionResult> Index()
    {
        var alojamientos = await _context.Alojamientos
            .Include(a => a.Tipo)
            .Include(a => a.DetallesAlojamientoComodidad)
                .ThenInclude(d => d.Comodidad)
            .ToListAsync();
        return View(alojamientos);
    }

    // GET: Alojamiento/Create
    public IActionResult Create()
    {
        try
        {
            var tipos = _context.Tipos.ToList();
            var comodidades = _context.Comodidades.Where(c => c.Estado).ToList();

            if (!tipos.Any())
            {
                TempData["Error"] = "No hay tipos de alojamiento disponibles. Por favor, cree algunos primero.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TipoId = new SelectList(tipos, "Id", "Nombre");
            ViewBag.Comodidades = new SelectList(comodidades, "Id", "Nombre");

            return View();
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Error al cargar el formulario: " + ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: Alojamiento/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Alojamiento alojamiento, IFormFile imagen, int[] ComodidadesSeleccionadas)
    {
     
        try
        {
            // Debug - ver qué valores están llegando
            var recibidos = new Dictionary<string, string>
        {
            { "Nombre", alojamiento.Nombre ?? "null" },
            { "Capacidad", alojamiento.Capacidad.ToString() },
            { "Descripcion", alojamiento.Descripcion ?? "null" },
            { "TipoId", alojamiento.TipoId.ToString() },
            { "Imagen", imagen != null ? "Presente" : "No presente" },
            { "ComodidadesSeleccionadas", ComodidadesSeleccionadas != null ?
                string.Join(",", ComodidadesSeleccionadas) : "null" }
        };

            ViewBag.RecibidosDebug = recibidos;

            // Si el ModelState no es válido, mostrar los errores específicos
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        errors.Add($"Campo: {modelStateKey} - Error: {error.ErrorMessage}");
                    }
                }

                ViewBag.TipoId = new SelectList(_context.Tipos, "Id", "Nombre", alojamiento.TipoId);
                ViewBag.Comodidades = new SelectList(_context.Comodidades.Where(c => c.Estado), "Id", "Nombre");
                ViewBag.Errors = errors;
                return View(alojamiento);
            }

            // Procesar la imagen si existe
            if (imagen != null && imagen.Length > 0)
            {
                var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images", "alojamientos");
                Directory.CreateDirectory(uploadDir); // Crear directorio si no existe

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }

                alojamiento.ImagenUrl = "/images/alojamientos/" + fileName;
            }
            else
            {
                // Asignar una imagen por defecto o dejar vacío
                alojamiento.ImagenUrl = "";
            }

            // Guardar el alojamiento
            _context.Alojamientos.Add(alojamiento);
            await _context.SaveChangesAsync();

            // Guardar las comodidades
            if (ComodidadesSeleccionadas != null && ComodidadesSeleccionadas.Length > 0)
            {
                foreach (var comodidadId in ComodidadesSeleccionadas)
                {
                    var detalle = new DetalleAlojamientoComodidad
                    {
                        IdAlojamiento = alojamiento.Id,
                        ComodidadId = comodidadId
                    };
                    _context.DetallesAlojamientoComodidad.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }

            TempData["Success"] = "Alojamiento creado exitosamente";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.TipoId = new SelectList(_context.Tipos, "Id", "Nombre", alojamiento.TipoId);
            ViewBag.Comodidades = new SelectList(_context.Comodidades.Where(c => c.Estado), "Id", "Nombre");
            ViewBag.Error = $"Error al crear el alojamiento: {ex.Message}\nStack Trace: {ex.StackTrace}";
            return View(alojamiento);
        }
    }

    // GET: Alojamiento/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var alojamiento = await _context.Alojamientos
            .Include(a => a.DetallesAlojamientoComodidad)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (alojamiento == null)
        {
            return NotFound();
        }

        ViewBag.TipoId = new SelectList(_context.Tipos, "Id", "Nombre", alojamiento.TipoId);
        ViewBag.Comodidades = new SelectList(_context.Comodidades.Where(c => c.Estado), "Id", "Nombre");
        return View(alojamiento);
    }

    // POST: Alojamiento/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Alojamiento alojamiento, IFormFile imagen, int[] ComodidadesSeleccionadas)
    {
        if (id != alojamiento.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                if (imagen != null && imagen.Length > 0)
                {
                    var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images/alojamientos");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                    var filePath = Path.Combine(uploadDir, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagen.CopyToAsync(fileStream);
                    }

                    // Eliminar imagen anterior si existe
                    if (!string.IsNullOrEmpty(alojamiento.ImagenUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, alojamiento.ImagenUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    alojamiento.ImagenUrl = "/images/alojamientos/" + fileName;
                }

                // Actualizar comodidades
                var existingDetails = await _context.DetallesAlojamientoComodidad
                    .Where(d => d.IdAlojamiento == id)
                    .ToListAsync();

                _context.DetallesAlojamientoComodidad.RemoveRange(existingDetails);

                if (ComodidadesSeleccionadas != null)
                {
                    foreach (var comodidadId in ComodidadesSeleccionadas)
                    {
                        var detalle = new DetalleAlojamientoComodidad
                        {
                            IdAlojamiento = alojamiento.Id,
                            ComodidadId = comodidadId
                        };
                        _context.DetallesAlojamientoComodidad.Add(detalle);
                    }
                }

                _context.Update(alojamiento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlojamientoExists(alojamiento.Id))
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

        ViewBag.TipoId = new SelectList(_context.Tipos, "Id", "Nombre", alojamiento.TipoId);
        ViewBag.Comodidades = new SelectList(_context.Comodidades.Where(c => c.Estado), "Id", "Nombre");
        return View(alojamiento);
    }

    // GET: Alojamiento/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var alojamiento = await _context.Alojamientos
            .Include(a => a.Tipo)
            .Include(a => a.DetallesAlojamientoComodidad)
                .ThenInclude(d => d.Comodidad)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (alojamiento == null)
        {
            return NotFound();
        }

        return View(alojamiento);
    }

    // POST: Alojamiento/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var alojamiento = await _context.Alojamientos.FindAsync(id);

        // Eliminar imagen si existe
        if (!string.IsNullOrEmpty(alojamiento.ImagenUrl))
        {
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, alojamiento.ImagenUrl.TrimStart('/'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        _context.Alojamientos.Remove(alojamiento);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AlojamientoExists(int id)
    {
        return _context.Alojamientos.Any(e => e.Id == id);
    }
}