using Microsoft.AspNetCore.Mvc;
using ProyectoGestionDispositivos.Models.Entidades;
using Microsoft.EntityFrameworkCore;
namespace ProyectoGestionDispositivos.Controllers
{
    [Route("[controller]/[action]")] // Agrega este atributo
    public class TipoMantenimientosController : Controller
    {
        private readonly LibreriaContext _context;

        public TipoMantenimientosController(LibreriaContext context)
        {
            _context = context;

        }

        [HttpGet] // Especifica que es un GET
        public async Task<IActionResult> Lista()
        {
            return View(await _context.TipoMantenimientos.ToListAsync());
        }

        [HttpGet] // Especifica que es un GET
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Crear(TipoMantenimiento tipoMantenimiento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(tipoMantenimiento);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Tipo de Mantenimiento registrado correctamente";
                    return RedirectToAction("Lista");
                }
                catch
                {
                    ModelState.AddModelError(String.Empty, "Ha ocurrido un error");
                }
            }
            return View(tipoMantenimiento);
        }

        [HttpGet("{id}")] // GET con parámetro
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || _context.TipoMantenimientos == null)
            {
                return NotFound();
            }

            var tipoMantenimiento = await _context.TipoMantenimientos.FindAsync(id);
            if (tipoMantenimiento == null)
            {
                return NotFound();
            }
            return View(tipoMantenimiento);
        }

        [HttpPost("{id}")] // POST con parámetro

        public async Task<IActionResult> Editar(int id, TipoMantenimiento tipoMantenimientos)
        {
            if (id != tipoMantenimientos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoMantenimientos);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Tipo de Mantenimiento actualizado correctamente";
                    return RedirectToAction("Lista");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.Message, "Ocurrio un error al acualizar");
                }
            }
            return View(tipoMantenimientos);
        }

        [HttpPost]
        [Route("/TipoMantenimientos/Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var tipoMantenimiento = await _context.TipoMantenimientos.FindAsync(id);
                if (tipoMantenimiento == null)
                {
                    return NotFound(new { success = false, message = "Registro no encontrado" });
                }

                _context.TipoMantenimientos.Remove(tipoMantenimiento);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Eliminación exitosa" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error interno: {ex.Message}"
                });
            }
        }
    }
}
