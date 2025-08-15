using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Materias.Dtos
{
    public class LeccionDto
    {
        public int IdLeccion { get; set; }
        public int IdCurso { get; set; }
        public string? Nombre { get; set; }
        public bool Estado { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
