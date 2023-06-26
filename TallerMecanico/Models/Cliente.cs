using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerMecanico.Models
{
    public class Cliente
    {
        public string IdCliente { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Campo es Requerido")]
        public string nombre { get; set; }
        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "Campo es Requerido")]
        public string direccion { get; set; }
        [Display(Name = "Codigo Postal")]
        [Required(ErrorMessage = "Campo es Requerido")]
        public string cp { get; set; }
        [Display(Name = "Telefono")]
        public string telefono { get; set; }
        [Display(Name = "RFC")]
        [Required(ErrorMessage = "Campo es Requerido")]
        public string rfc { get; set; }
        [Display(Name = "Correo")]
        public string correo { get; set; }
    }
}
