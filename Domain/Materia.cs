namespace Domain
{
    public class Materia
    {
        public int IdMateria { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
    }
}
