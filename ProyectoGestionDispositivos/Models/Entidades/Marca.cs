using System;
using System.ComponentModel.DataAnnotations;
namespace ProyectoGestionDispositivos.Models.Entidades
{
    public class Marca
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        
        public string NombreMarca { get; set; }

        [DisplayFormat(DataFormatString ="0:yyyy/MM/dd hh:mm tt")]

        public DateTime FechaRegistro { get; set; }

    }
}
