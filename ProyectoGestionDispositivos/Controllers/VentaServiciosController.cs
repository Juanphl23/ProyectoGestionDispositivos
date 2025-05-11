using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoGestionDispositivos.Models;
using ProyectoGestionDispositivos.Models.Entidades;
using ProyectoGestionDispositivos.Services;


namespace ProyectoGestionDispositivos.Controllers
{
    public class VentaServiciosController : Controller
    {
        private readonly LibreriaContext _context;

        public VentaServiciosController(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Lista()
        {
            decimal sumaTotal = _context.VentaServicios.Sum(v => v.Total);
            ViewBag.SumaTotal = sumaTotal;
            decimal sumaDiaria = _context.VentaServicios
            .Where(v => v.Fecha.Date == DateTime.Today)
            .Sum(v => v.Total);
            ViewBag.SumaDiaria = sumaDiaria;
            return View(await _context.VentaServicios
                .Include(v => v.Dispositivo).ThenInclude(l => l.Marca)
                .Include(v => v.Dispositivo).ThenInclude(l => l.TipoMantenimiento)
                .Include(v => v.Usuario)
                .ToListAsync());
        }
    }
}
