using Application.OpcionesRpts.Dtos;

namespace Application.Preguntas.Dto
{
    public class QuestionRequestDto
    {
        public PreguntaSaveDto? PreguntaSaveDto { get; set; }
        public IList<OpcionSaveDto>? OpcionSaveDto { get; set; }
    }
}
