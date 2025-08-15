namespace Application.Materias.Dtos.SaveDtos
{
    public class CursoLeccionSaveDto
    {
        public CursoSaveDto? CursoSaveDto { get; set; }
        public IList<LeccionSaveDto>? LeccionSaveDtoList { get; set; }
    }
}
