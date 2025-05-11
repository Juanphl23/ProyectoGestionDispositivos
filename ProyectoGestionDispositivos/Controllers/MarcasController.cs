using Microsoft.AspNetCore.Mvc;
using ProyectoGestionDispositivos.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ProyectoGestionDispositivos.Controllers
{
    [Route("[controller]/[action]")] // Agrega este atributo
    
    public class MarcasController : Controller
    {

        private readonly LibreriaContext _context;

        public MarcasController(LibreriaContext context)
        {
            _context = context;
        }

        [HttpGet] // Especifica que es un GET
        public async Task <IActionResult> Lista()
        {
            return View(await _context.Marcas.ToListAsync());
        }

        [HttpGet] // GET para mostrar formulario
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Crear(Marca marca)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(marca);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Marca registrada correctamente";
                    return RedirectToAction("Lista");
                }
                catch
                {
                    ModelState.AddModelError(String.Empty, "Ha ocurrido un error");
                }
            }
            return View(marca);
        }

        [HttpGet("{id}")] // GET con parámetro
        public async Task<IActionResult> Editar (int? id)
        {
            if(id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FindAsync(id);
            if( marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        [HttpPost("{id}")] // POST con parámetro

        public async Task<IActionResult> Editar(int id, Marca marca)
        {
            if( id!= marca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marca);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Marca actualizada correctamente";
                    return RedirectToAction("Lista");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.Message, "Ocurrio un error al acualizar");
                }
            }
            return View(marca);
        }

        [HttpPost]
        [Route("/Marcas/Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var marca = await _context.Marcas.FindAsync(id);
                if (marca == null)
                {
                    return NotFound(new { success = false, message = "Registro no encontrado" });
                }

                _context.Marcas.Remove(marca);
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
