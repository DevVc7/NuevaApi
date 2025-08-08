namespace Application.OpcionesRpts.Dtos
{
    public class OpcionSaveDto
    {
        public int IdOpcion { get; set; }
        public string? Texto { get; set; }
        public bool EsCorrecta { get; set; }
    }
}
