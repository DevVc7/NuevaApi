namespace Application.Preguntas.Dto
{
    public class RespuestaUsuarioSaveDto
    {
        public int IdUsuario { get; set; }
        public int IdPregunta { get; set; }
        public int IdOpcion { get; set; }
        public int TiempoRespuesta { get; set; }
        public decimal PuntajeObtenido { get; set; }
    }
}
