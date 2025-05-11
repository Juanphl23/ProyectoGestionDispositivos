using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoGestionDispositivos.Models.Entidades;

namespace ProyectoGestionDispositivos.Services
{
    public class ServicioLista : IServicioLista
    {
        private readonly LibreriaContext _context;

        public ServicioLista(LibreriaContext context)
        {
            _context = context;
        }


         public async Task<IEnumerable<SelectListItem>> GetListaMarcas()
        {
            List<SelectListItem> list = await _context.Marcas.Select(x => new SelectListItem
            {
                Text = x.NombreMarca,
                Value = $"{x.Id}"
            })
                .OrderBy(x => x.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Marca...]",
                Value = "0"
            });

                return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetListaTipoMantenimientos()
        {
            List<SelectListItem> list = await _context.TipoMantenimientos.Select(x => new SelectListItem
            {
                Text = x.NombreTipo,
                Value = $"{x.Id}"
            })
                .OrderBy(x => x.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text= "[Seleccione un Tipo de Equipo...]",
                Value ="0"
            });

            return list;
        }
    }
}
