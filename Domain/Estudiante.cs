using System.Text.Json.Serialization;

namespace Domain
{
    public class Estudiante
    {
		public int IdEstudiante { get; set; }
        public int IdUsuario {  get; set; }
        public string? CodEstudiante { get; set; }
        public string? Descripcion { get; set; }
        public int IdGrado { get; set; }
        public int? IdSeccion {  get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }

        public virtual User? Usuario { get; set; }
        public virtual Grado? Grado { get; set; }

    }
}