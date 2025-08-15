using Domain;

namespace Application.Preguntas.Dto
{
    public class PreguntaDto
    {
        public int IdPregunta { get; set; }
        public string? Dificultad {  get; set; }
        public string? Enunciado { get; set; }
        public string? Explicacion { get; set; }
        public string? Pista { get; set; }
        public decimal? Puntaje {  get; set; }
        public bool Estado {  get; set; }
        public int? IdGrado { get; set; }
        public int? IdMateria { get; set; }
        public int? IdCurso { get; set; }
        public int? IdLeccion { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual Grado? Grado { get; set; }
        public virtual Materia? Materia { get; set; }
        public virtual ICollection<OpcionesRpt>? OpcionesRpt { get; set; }

    }
}
