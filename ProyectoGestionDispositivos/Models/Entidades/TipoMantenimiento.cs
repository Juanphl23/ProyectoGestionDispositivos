using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoGestionDispositivos.Models.Entidades
{
    public class TipoMantenimiento
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es obligatorio")]

        public string NombreTipo { get; set; }

        [DataType(DataType.MultilineText)]

        public string Descripcion { get; set; }

    }
}
