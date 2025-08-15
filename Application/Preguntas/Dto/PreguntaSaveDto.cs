namespace Application.Preguntas.Dto
{
    public class PreguntaSaveDto
    {
        public string? Dificultad { get; set; }
        public string? Enunciado { get; set; }
        public string? Explicacion { get; set; }
        public string? Pista { get; set; }
        public decimal? Puntaje { get; set; }
        public int? IdGrado { get; set; }
        public int? IdMateria { get; set; }
        public int? IdCurso { get; set; }
        public int? IdLeccion { get; set; }
    }
}
