using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.View
{
    public class ReporteUsuarioDto
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string DificultadDominante { get; set; }
        public string Message { get; set; }
        public decimal TotalPuntaje { get; set; }
    }

        public class ReporteUsuario
        {
            public int IdUsuario { get; set; }
            public string Enunciado { get; set; }
            public string Dificultad { get; set; }
            public decimal PuntajeObtenido { get; set; }
            public int TiempoRespuesta { get; set; }
        }
}
