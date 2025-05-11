using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ProyectoGestionDispositivos.Models.Entidades
{
    public class Dispositivo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]

        public string NombreDispositivo { get; set; }

        public Marca Marca { get; set; }

        public TipoMantenimiento TipoMantenimiento { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]

        public decimal Costo { get; set; }

        

        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una marca.")]
        public int MarcaId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un Tipo de Mantenimiento.")]

        public int TipoMantenimientoId { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> TipoMantenimientos { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Marcas { get; set; }
    }
}
