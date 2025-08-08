namespace Application.Estudiantes.Dtos.Grados
{
    public class GradoDto
    {
        public int IdGrado { get; set; }
        public string? Titulo { get; set; }
        public bool Estado { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
