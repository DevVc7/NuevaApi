using Domain;

namespace Application.Estudiant.Dtos.Students
{
    public class EstudianteDto
    {
        public int IdEstudiante { get; set; }
        public int IdUsuario { get; set; }
        public string? CodEstudiante { get; set; }
        public string? Descripcion { get; set; }
        public int IdGrado { get; set; }
        public int IdSeccion { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public User? Usuario { get; set; }
        public Grado? Grado { get; set; }
    }
}
