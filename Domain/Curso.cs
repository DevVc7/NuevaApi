using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Curso
    {
        public int? IdCurso { get; set; }
        public int? IdMateria { get; set; }
        public string? Nombre { get; set; }
        public bool Estado { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }

        [JsonIgnore]
        public virtual Materia? Materia { get; set; }
    }
}
