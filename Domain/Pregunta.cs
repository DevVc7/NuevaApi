using System.Collections.ObjectModel;

namespace Domain
{
    public class Pregunta
    {
        public int IdPregunta { get; set; }
        public string? Dificultad { get; set; }
        public string? Enunciado { get; set; }
        public string? Explicacion { get; set; }
        public string? Pista { get; set; }
        public decimal? Puntaje { get; set; }
        public bool Estado { get; set; }
        public int? IdGrado { get; set; }
        public int? IdMateria { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Grado? Grado { get; set; }
        public virtual Materia? Materia { get; set; }
        public virtual ICollection<OpcionesRpt>? OpcionesRpt { get; set; }
    }
}
