using System.Text.Json.Serialization;

namespace Domain
{
    public class Leccion
    {
        public int? IdLeccion { get; set; }
        public int? IdCurso { get; set; }
        public string? Nombre { get; set; }
        public bool Estado { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        [JsonIgnore]
        public virtual Curso? Curso { get; set; }
    }
}
