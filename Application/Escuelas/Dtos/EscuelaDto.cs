using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Escuelas.Dtos
{
    public class EscuelaDto
    {
        public int IdEscuela { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Codigo { get; set; }
        public string? Correo { get; set; }
        public string? Descripcion { get; set; }
    }
}
