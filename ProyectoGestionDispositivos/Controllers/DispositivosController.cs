using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoGestionDispositivos.Models;
using ProyectoGestionDispositivos.Models.Entidades;
using ProyectoGestionDispositivos.Services;

namespace ProyectoGestionDispositivos.Controllers
{
    public class DispositivosController : Controller
    {
        private readonly LibreriaContext _context;
        private readonly IServicioLista _servicioLista;
        //private readonly IServicioImagen _servicioImagen;
        private readonly IServicioUsuario _servicioUsuario;

        public DispositivosController(LibreriaContext context,
            IServicioLista servicioLista,
            //IServicioImagen servicioImagen,
            IServicioUsuario servicioUsuario)
        {
            _context = context;
            _servicioLista = servicioLista;
            //_servicioImagen = servicioImagen;
            _servicioUsuario = servicioUsuario;
        }

        public async Task<IActionResult> Lista()
        {
            return View(await _context.Dispositivos
                .Include(l => l.TipoMantenimiento)
                .Include(l => l.Marca)
                .ToListAsync());
        }

        public async Task<IActionResult> Crear()
        {
            Dispositivo dispositivo = new()
            {
                TipoMantenimientos = await _servicioLista.GetListaTipoMantenimientos(),
                Marcas = await _servicioLista.GetListaMarcas(),
            };

            return View(dispositivo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Dispositivo dispositivo)
        {
            if (ModelState.IsValid)
            {
                //string urlimagen = await _servicioImagen.SubirImagen(image, Imagen.FileName);

                try
                {

                    //dispositivo.URLImagen = urlimagen;

                    _context.Add(dispositivo);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Dispositivo creado exitosamente!!!";
                    return RedirectToAction("Lista");
                }
                catch
                {
                    ModelState.AddModelError(String.Empty, "Ha ocurrido un error");
                }
            }
            dispositivo.TipoMantenimientos = await _servicioLista.GetListaTipoMantenimientos();
            dispositivo.Marcas = await _servicioLista.GetListaMarcas();
            return View(dispositivo);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispositivo = await _context.Dispositivos.FindAsync(id);
            if (dispositivo == null)
            {
                return NotFound();
            }

            dispositivo.TipoMantenimientos = await _servicioLista.GetListaTipoMantenimientos();
            dispositivo.Marcas = await _servicioLista.GetListaMarcas();

            return View(dispositivo);
        }

        
        [HttpPost]
        public async Task<IActionResult> Editar(Dispositivo dispositivo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dispositivoExistente = await _context.Dispositivos.FindAsync(dispositivo.Id);
                    if (dispositivoExistente == null)
                    {
                        return NotFound();
                    }
                                        

                    dispositivoExistente.NombreDispositivo = dispositivo.NombreDispositivo;
                    dispositivoExistente.Marca = await _context.Marcas.FindAsync(dispositivo.MarcaId);
                    dispositivoExistente.TipoMantenimiento = await _context.TipoMantenimientos.FindAsync(dispositivo.TipoMantenimientoId);
                    dispositivoExistente.Costo = dispositivo.Costo;

                    _context.Update(dispositivoExistente);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Dispositivo agregado exitosamente!!!";
                    return RedirectToAction("Lista");
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Ha ocurrido un error");
                }
            }

            dispositivo.TipoMantenimientos = await _servicioLista.GetListaTipoMantenimientos();
            dispositivo.Marcas = await _servicioLista.GetListaMarcas();
            return View(dispositivo);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Dispositivos == null)
            {
                return NotFound();
            }

            var dispositivo = await _context.Dispositivos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (dispositivo == null)
            {
                return NotFound();
            }

            try
            {
                _context.Dispositivos.Remove(dispositivo);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Dispositivo eliminado exitosamente!!!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrio un error, no se pudo eliminar el registro");
            }

            return RedirectToAction(nameof(Lista));
        }

        public async Task<IActionResult> Vender(int id)
        {

            Usuario usuario = await _servicioUsuario.GetUsuario(User.Identity.Name);
            if (usuario == null)
            {
                return NotFound();
            }

            var dispositivo = await _context.Dispositivos.FindAsync(id);

            if (dispositivo == null)
            {
                return NotFound();
            }

            VentaServicioViewModel model = new()
            {
                Dispositivo = dispositivo,
                DispositivoId = id,
                NombreDispositivo = dispositivo.NombreDispositivo,
                Costo = dispositivo.Costo,                
                Usuario = usuario,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Vender(VentaServicioViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _servicioUsuario.GetUsuario(User.Identity.Name);
                if (usuario == null)
                {
                    return NotFound();
                }

                Dispositivo dispositivo = await _context.Dispositivos.FindAsync(model.DispositivoId);
                if (dispositivo == null)
                {
                    return NotFound();
                }
                try
                {
                    VentaServicio ventaServicio = new()
                    {
                        Dispositivo = dispositivo,
                        Fecha = DateTime.Today,
                        Cantidad = model.Cantidad,
                        Total = dispositivo.Costo * (decimal)model.Cantidad,
                        Usuario = usuario,
                    };

                    _context.Add(ventaServicio);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Servicio vendido exitosamente!!!";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.Message, "Ocurrio un error");
                };
                return RedirectToAction("Lista");
            }
            return View(model);
        }
    }

    
} 
