using Domain;

namespace Application.Materias.Dtos
{
    public class CursoDto
    {
        public int IdCurso { get; set; }
        public int IdMateria { get; set; }
        public string? Nombre { get; set; }
        public bool Estado { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual Materia? Materia { get; set; }
    }
}
