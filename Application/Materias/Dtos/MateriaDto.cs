using Domain;

namespace Application.Materias.Dtos
{
    public class MateriaDto
    {
        public int IdMateria { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public virtual ICollection<Curso>? Cursos { get; set; }
    }
}
