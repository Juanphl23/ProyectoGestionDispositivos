using ProyectoGestionDispositivos.Models.Entidades;
using System.ComponentModel.DataAnnotations;

namespace ProyectoGestionDispositivos.Models
{
    public class VentaServicioViewModel
    {
        public string NombreDispositivo { get; set; }
        public Dispositivo Dispositivo { get; set; }
        public int DispositivoId { get; set; }
        public decimal Costo { get; set; }

        public string URLImagen { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
        public Usuario Usuario { get; set; }
    }
}
