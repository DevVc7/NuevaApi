using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Preguntas.Dto
{
    public class RespuestaUsuarioDto
    {
        public int IdRespuesta { get; set; }
        public int IdUsuario { get; set; }
        public int IdPregunta { get; set; }
        public int IdOpcion { get; set; }
        public DateTime FechaRespuesta { get; set; }
        public int TiempoRespuesta { get; set; }
        public decimal PuntajeObtenido { get; set; }

        public virtual OpcionesRpt? OpcionesRpt { get; set; }
        public virtual Pregunta? Pregunta { get; set; }


    }
}
