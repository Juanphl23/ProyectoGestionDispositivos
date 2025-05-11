using Microsoft.AspNetCore.Mvc.Rendering;
namespace ProyectoGestionDispositivos.Services
{
    public interface IServicioLista
    {
        Task<IEnumerable<SelectListItem>> GetListaMarcas();
        Task<IEnumerable<SelectListItem>> GetListaTipoMantenimientos();
    }
}
